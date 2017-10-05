using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WPFDBApp.Services.TreeServices;

namespace WPFDBApp.ValueConverter
{
    [ValueConversion(typeof(TreeItemType), typeof(BitmapImage))]
    public class TreeViewImageConverter : IValueConverter
    {
        public static TreeViewImageConverter Instance = new TreeViewImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // By default, we presume an image
            var image = "Images/Folder.png";

            switch ((TreeItemType)value)
            {
                case TreeItemType.Server:
                    image = "Images/Server.png";
                    break;
                case TreeItemType.DataBase:
                    image = "Images/Database.png";
                    break;
                case TreeItemType.Table:
                    image = "Images/TableHS.png";
                    break;
                case TreeItemType.View:
                    image = "Images/TableView.png";
                    break;
                case TreeItemType.Procedure:
                    image = "Images/Procedure.png";
                    break;
                case TreeItemType.PrimaryKey:
                    image = "Images/pkey-32.png";
                    break;
                case TreeItemType.ForeignKey:
                    image = "Images/fkey-32.png";
                    break;
                case TreeItemType.Column:
                    image = "Images/table_column.png";
                    break;
                case TreeItemType.Index:
                    image = "Images/index.png";
                    break;
                case TreeItemType.DefaultConstraint:
                case TreeItemType.CheckConstraint:
                    image = "Images/constraint.png";
                    break;
                case TreeItemType.Parameter:
                    image = "Images/param.png";
                    break;
                case TreeItemType.Dummy:
                    image = "Images/loading.gif";
                    break;
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
