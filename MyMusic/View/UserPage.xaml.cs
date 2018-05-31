using MyMusic.Model;
using MyMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public UserViewModel UserMusicVm => (UserViewModel)DataContext;
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        internal PersonInfoViewModel user = PersonInfoViewModel.GetInstance();
        const int leftWidth = 180;
        const int rightWidth = 180;
        const int portraitSize = 120;
        Thickness narrowPortrait = new Thickness(0, 140, 20, 0);
        Thickness widePortrait = new Thickness(30, 100, 0, 0);

        public UserPage()
        {
            this.InitializeComponent();
        }

        private async void pickBackground(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            BitmapImage bitmap = new BitmapImage();
            try
            {
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmap.SetSource(stream);
                }
            }
            catch (Exception)
            {
                return;
            }
            backgroundPictrure.Source = bitmap;
            StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
            StorageFile saveFile = await applicationFolder.CreateFileAsync(file.Name, CreationCollisionOption.OpenIfExists);
            RenderTargetBitmap bitmap2 = new RenderTargetBitmap();
            await bitmap2.RenderAsync(backgroundPictrure);
            var pixelBuffer = await bitmap2.GetPixelsAsync();
            try
            {
                using (var fileStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = null;
                    if (file.FileType == ".jpg" || file.FileType == ".jepg") encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, fileStream);
                    else encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);
                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Ignore,
                        (uint)bitmap2.PixelWidth,
                        (uint)bitmap2.PixelHeight,
                        DisplayInformation.GetForCurrentView().LogicalDpi,
                        DisplayInformation.GetForCurrentView().LogicalDpi,
                        pixelBuffer.ToArray()
                        );
                    await encoder.FlushAsync();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private new void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*if (e.NewSize.Width < 430)
            {
                leftInfor.Visibility = (Visibility)1;
                rightInfor.Visibility = (Visibility)1;
            }
            else if(e.NewSize.Width < 550)
            {
                leftInfor.Visibility = 0;
                rightInfor.Visibility = 0;
                leftInfor.Width = leftWidth * 2 / 3;
                rightInfor.Width = rightWidth * 2 / 3;
                portrait.Width = portraitSize / 2;
                portrait.Height = portraitSize / 2;
                portrait.HorizontalAlignment = HorizontalAlignment.Right;
                portrait.Margin = narrowPortrait;
            }
            else if (e.NewSize.Width > 550)
            {
                leftInfor.Width = leftWidth;
                rightInfor.Width = rightWidth;
                leftInfor.Visibility = 0;
                rightInfor.Visibility = 0;
                portrait.Width = portraitSize;
                portrait.Height = portraitSize;
                portrait.HorizontalAlignment = HorizontalAlignment.Left;
                portrait.Margin = widePortrait;
            }*/
        }
    }
}
