using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
// ReSharper disable ConvertToUsingDeclaration
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable StaticMemberInGenericType
#pragma warning disable CS0414
#pragma warning disable CS1591

namespace GongSolutions.Shell
{
    // TODO: static 덜어내고 생성해서 쓰기.
    public class CachedObject<T>
    {
        private static readonly Stack<CachedObject<T>> _pool = new Stack<CachedObject<T>>();
        private static readonly object _lock = new object();

        public string Name { get; set; }
        public T Object { get; set; }

        private CachedObject(string name, T obj)
        {
            Name = name;
            Object = obj;
        }

        public static CachedObject<T> Get(string parsingName, T obj)
        {
            lock (_lock)
            {
                if (_pool.Count > 0)
                {
                    var cachedObject = _pool.Pop();
                    cachedObject.Name = parsingName;
                    cachedObject.Object = obj;
                    return cachedObject;
                }
                else
                {
                    return new CachedObject<T>(parsingName, obj);
                }
            }
        }

        public void ReturnToPool()
        {
            lock (_lock)
            {
                _pool.Push(this);
            }
        }
    }
    
    /// <summary>
    /// The ShellPreviewPanel class is a custom Panel derived from the standard Panel control.
    /// This class is intended to provide a specialized panel for displaying shell preview content.
    /// </summary>
    public class ShellPreviewPanel : Panel
    {
        private ShellView _shellView;
        private readonly PictureBox _pictureBox = new PictureBox();
        private CachedObject<Image> _cached;
        // private LocalizationService _localService;

        private readonly string SELECT_FILES = "파일을 선택해주세요.";
        private readonly string CANNOT_PREVIEW = "미리 볼 수 없습니다.";
        
        private void InitializeComponent()
        {
            // _localService = new LocalizationService();
            _shellView.SelectionChanged += (sender, args) => RefreshContent();
            // _shellView.ItemDeleted += ShellViewOnItemDeleted;
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(_pictureBox);
        }

        /// <summary>
        /// Gets or sets the ShellView associated with this ShellPreviewPanel.
        /// </summary>
        /// <remarks>
        /// When a new ShellView is assigned, the ShellPreviewPanel will automatically
        /// initialize its components and refresh the displayed content.
        /// </remarks>
        [DefaultValue(null), Category("Behaviour")]
        public ShellView ShellView
        {
            get => _shellView;
            set
            {
                _shellView = value;

                if (_shellView == null) 
                    return;
                
                InitializeComponent();
                RefreshContent();
            }
        }

        private void ShellViewOnItemDeleted(object sender, EventArgs e)
        {
            _pictureBox.Image = null;
            _cached = null;
            RefreshContent();
        }

        public void RefreshContent()
        {
            try
            {
                var item = _shellView.SelectedItems?.Last();

                if (item == null)
                    throw new Exception();

                if (_cached?.Name == item.ParsingName)
                {
                    _pictureBox.Image = _cached?.Object;
                    return;
                }

                ClearString();

                using (var stream = new FileStream(item.ParsingName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var bufferedStream = new BufferedStream(stream))
                {
                    if (_cached?.Name == item.ParsingName) // 이미 캐시된 파일인 경우
                    {
                        _pictureBox.Image = _cached?.Object;
                    }
                    else
                    {
                        var image = new Bitmap(bufferedStream);
                        _pictureBox.Image = image;
                        _cached?.ReturnToPool(); // 이미 사용한 CachedFile 객체를 다시 객체 풀에 반환
                        _cached = CachedObject<Image>.Get(item.ParsingName, image); // 새로운 CachedFile 객체를 가져오거나, 객체 풀에서 재사용합니다.
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                DrawString(SELECT_FILES);
                // DrawString(_localService.GetStrings("Select_Files"));
            }
            catch (FileNotFoundException ex)
            {
                DrawString(SELECT_FILES);
                // DrawString(_localService.GetStrings("Select_Files"));
            }
            catch (Exception ex)
            {
                DrawString(CANNOT_PREVIEW);
                // NullReferenceException에선 LocalizationService초기화를 다시해야함.
                // _localService = new LocalizationService();
                // DrawString(_localService.GetStrings("Cannot_Preview"));
            }
            finally
            {
                if (_pictureBox.Image != null)
                    SetImageSizeAndLocation();
            }
        }
        
        private void HandleException(Exception ex)
        {
            string message;

            switch (ex)
            {
                case InvalidOperationException _:
                case FileNotFoundException _:
                {
                    message = "파일을 선택해주세요.";
                    break;
                }
                case ArgumentException _:
                case NullReferenceException _:
                case IndexOutOfRangeException _:
                {
                    message = "미리 볼 수 없습니다.";
                    break;
                }
                default:
                {
                    message = "미리 볼 수 없습니다.";
                    break;
                }
            }

            Console.WriteLine($@"{GetCallingFunctionName(2)}, {ex}");
            DrawString(message);
        }

        private static string GetCallingFunctionName(int index)
        {
            var stackTrace = new StackTrace();
            var callingFunction = stackTrace.GetFrame(index).GetMethod().Name;
            return callingFunction;
        }
        
        private void LoadImage(string filePath)
        {
            try
            {
                using (var memStream = new MemoryStream())
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    stream.CopyTo(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    var image = Image.FromStream(memStream);

                    if (Path.GetExtension(filePath).Equals(".gif"))
                    {
                        _pictureBox.Image = image.GetThumbnailImage(image.Width, image.Height, null, IntPtr.Zero);
                    }
                    else
                    {
                        _pictureBox.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DrawString(string message)
        {
            _pictureBox.Size = Size.Empty;
            _pictureBox.Image = null;
            _cached = null;

            try
            {
                using (var g = this.CreateGraphics())
                {
                    g.Clear(this.BackColor);
                    var messageSize = g.MeasureString(message, SystemFonts.DefaultFont);
                    var messageLocation = new PointF((this.Width - messageSize.Width) / 2,
                        (this.Height - messageSize.Height) / 2);
                    g.DrawString(message, SystemFonts.DefaultFont, Brushes.Black, messageLocation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ClearString()
        {
            using (var g = this.CreateGraphics())
            {
                g.Clear(this.BackColor);
            }
        }
        
        private void SetImageSizeAndLocation()
        {
            var widthRatio = (double)_pictureBox.Image.Width / (double)_pictureBox.Image.Height;
            var heightRatio = (double)_pictureBox.Image.Height / (double)_pictureBox.Image.Width;

            var maxWidth = _pictureBox.Parent.ClientSize.Width - 40;
            var maxHeight = _pictureBox.Parent.ClientSize.Height - 40;
            var newWidth = (int)(maxHeight * widthRatio);
            var newHeight = (int)(maxWidth * heightRatio);
                
            if (newWidth <= _pictureBox.Image.Width || newHeight <= _pictureBox.Image.Height)
            {
                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)(newWidth * heightRatio);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)(newHeight * widthRatio);
                }
                _pictureBox.Size = new Size(newWidth, newHeight);
            }
            else
            {
                _pictureBox.Size = _pictureBox.Image.Size;
            }

            _pictureBox.Location = new Point(
                _pictureBox.Parent.ClientSize.Width / 2 - _pictureBox.Size.Width / 2,
                _pictureBox.Parent.ClientSize.Height / 2 - _pictureBox.Size.Height / 2);
        }

        #region Winform Event

        protected override void OnPaint(PaintEventArgs e)
        {
            RefreshContent();
            base.OnPaint(e);
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        #endregion
    }
}