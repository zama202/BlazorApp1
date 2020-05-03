using BlazorApp1.Model;
using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        protected ProductService ProductService { get; set; }

        protected List<Product> products;
        protected override async Task OnInitializedAsync()
        {
            products = await ProductService.GetProductsAsync();
        }


        protected Product objProduct = new Product();
        protected bool ShowPopup = false;

        protected void ClosePopup()
        {
            // Close the Popup
            ShowPopup = false;
        }
        protected void AddNewProduct()
        {
            // Make new forecast
            objProduct = new Product();
            // Set Id to 0 so we know it is a new record
            objProduct.ProductionId = 0;
            // Open the Popup
            ShowPopup = true;
        }

        protected void EditProduct(Product product)
        {
            // Set the selected product
            // as the current product
            objProduct = product;
            // Open the Popup
            ShowPopup = true;
        }

        protected async Task DeleteProduct()
        {
            // Close the Popup
            ShowPopup = false;
            // Delete the forecast
            var result = ProductService.DeleteProductAsync(objProduct);
            // Get the forecasts for the current user
            products = await ProductService.GetProductsAsync();
        }

        protected async Task SaveProduct()
        {
            // Close the Popup
            ShowPopup = false;
            // A new forecast will have the Id set to 0
            if (objProduct.ProductionId == 0)
            {
                // Create new forecast
                Product objNewProduct = new Product();
                objNewProduct.ProductDate = System.DateTime.Now;
                objNewProduct.ProductQuantity = Convert.ToInt32(objProduct.ProductQuantity);
                objNewProduct.ProductDescription = objProduct.ProductDescription;
                objNewProduct.ProductCode = objProduct.ProductCode;
                // Save the result
                var result =
                ProductService.CreateForecastAsync(objNewProduct);
            }
            else
            {
                // This is an update
                var result = ProductService.UpdateProductAsync(objProduct);
            }
            products = await ProductService.GetProductsAsync();
        }
    }
}
