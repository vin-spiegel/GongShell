using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace GongSolutions.Shell.Interop
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("26b79130-4c9f-4424-aefb-52cc63f4d3c6")]
    public interface IContextMenuModifier
    {
        [PreserveSig]
        HResult GetContextMenu(IContextMenu oldMenu, out IContextMenu menu);
    }
}