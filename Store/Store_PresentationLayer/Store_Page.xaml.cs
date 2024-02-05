using Store_ApplicationLayer.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class Store_Page : UserControl
    {

        MainWindow main;
        List<Model_Product> products;

        public Store_Page(MainWindow main, List<Model_Product> products)
        {
            InitializeComponent();
            this.main = main;
            this.products = products;
            initFill();
        }

        private void initFill()
        {
            foreach (Model_Product product in products)
            {
                if (product != null)
                {
                    stackPanel.Children.Add(new Product(main, product.name, product.price.ToString(), product.url, product.id));
                }
            }
        }
    }
}
