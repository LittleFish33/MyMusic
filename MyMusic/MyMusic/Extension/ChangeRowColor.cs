using MyMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MyMusic.Extension
{
    public static class ChangeRowColor
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.RegisterAttached(
            "Color",
            typeof(Brush),
            typeof(ChangeRowColor),
            new PropertyMetadata(null, OnColorPropertyChanged));
        public static Brush GetColor(ListViewBase obj)
        {
            return (Brush)obj.GetValue(ColorProperty);
        }

        public static void SetColor(ListViewBase obj, Brush value)
        {
            obj.SetValue(ColorProperty, value);
        }

        private static void OnColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ColorContainerContentChanging;

            if (ColorProperty != null)
            {
                listViewBase.ContainerContentChanging += ColorContainerContentChanging;
            }
        }

        private static void ColorContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.Background = GetColor(sender);
            }
            else
            {
                itemContainer.Background = null;
            }
        }
    }
}
