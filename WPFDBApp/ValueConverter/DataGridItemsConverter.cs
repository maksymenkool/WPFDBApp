using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace WPFDBApp.ValueConverter
{
    public class DataGridItemsConverter : IValueConverter
    {
        public static DataGridItemsConverter Instance = new DataGridItemsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, string> convertdic = null;
            var result = value as Dictionary<string, string>;
            if (result != null)
            {
                convertdic = new Dictionary<string, string>();
                foreach (var item in result)
                {
                    if (item.Key != "xmlns" && item.Key != "is_selected" && item.Key != "is_expanded" && item.Key != "sql_script" && item.Key != "name1" && item.Key != "definition" && item.Key != "is_empty")
                        convertdic[item.Key] = item.Value;
                }
            }
            return convertdic;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
