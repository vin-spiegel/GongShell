using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
// ReSharper disable ConvertToUsingDeclaration

namespace GongSolutions.Shell
{
    /// <summary>
    /// The ShellPreviewPanel class is a custom Panel derived from the standard Panel control.
    /// This class is intended to provide a specialized panel for displaying shell preview content.
    /// </summary>
    public class ShellPreviewPanel : Panel
    {
        private ShellView _shellView;
        private readonly PictureBox _pictureBox = new PictureBox();
        private ShellItem _cached;
        // private LocalizationService _localService;
        
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

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RefreshContent();
        }

        public static string GetCallingFunctionName()
        {
            StackTrace stackTrace = new StackTrace();
            string callingFunction = stackTrace.GetFrame(2).GetMethod().Name;
            return callingFunction;
        }
        
        public void RefreshContent()
        {
            try
            {

                if (_shellView.SelectedItems.Length == 0)
                {
                    DrawString("파일을 선택해주세요.");
                    return;
                }
                else
                {
                    ClearString();
                }

                var item = _shellView.SelectedItems[_shellView.SelectedItems.Length - 1];

                if (_cached == item)
                    return;

                if (item != null)
                {
                    using (var memStream = new MemoryStream())
                    using (var stream = new FileStream(item.ParsingName, FileMode.Open, FileAccess.Read,
                               FileShare.ReadWrite))
                    {
                        stream.CopyTo(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);
                        _pictureBox.Image = Image.FromStream(memStream);
                    }

                }

                _cached = item;
            }
            catch (InvalidOperationException ex)
            {
                // DrawString(_localService.GetStrings("Select_Files"));
                Console.WriteLine($@"{GetCallingFunctionName()}, {ex}");
                DrawString("파일을 선택해주세요.");
            }
            catch (FileNotFoundException ex)
            {
                // DrawString(_localService.GetStrings("Select_Files"));
                DrawString("파일을 선택해주세요.");
            }
            catch (ArgumentException ex)
            {
                DrawString("미리 볼 수 없습니다.");
            }
            catch (NullReferenceException ex)
            {
                // NullReferenceException에선 LocalizationService초기화를 다시해야함.
                // _localService = new LocalizationService();
                // DrawString(_localService.GetStrings("Cannot_Preview"));
                DrawString("미리 볼 수 없습니다.");
            }
            catch (IndexOutOfRangeException ex)
            {
                DrawString("미리 볼 수 없습니다.");
            }
            catch (Exception ex)
            {
                DrawString("미리 볼 수 없습니다.");
            }
            finally
            {
                if (_pictureBox.Image != null)
                    SetImageSizeAndLocation();
            }
        }

        private string _currentMessage;
        private bool _initialized;

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
    }
}