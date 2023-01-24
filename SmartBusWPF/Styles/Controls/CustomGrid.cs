using System.Windows;
using System.Windows.Controls;

namespace SmartBusWPF.Styles.Controls
{
    public class CustomGrid : Grid
    {
        public CustomGrid()
        {
            CreateRows();
            CreateColumns();
        }

        private void CreateRows()
        {
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(150) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
        }

        private void CreateColumns()
        {
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }
}