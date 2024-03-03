# IO.Swagger.Api.BookStoreProductAPIApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddProduct**](BookStoreProductAPIApi.md#addproduct) | **POST** /products | 
[**GetProduct**](BookStoreProductAPIApi.md#getproduct) | **GET** /products/{id} | 
[**GetProducts**](BookStoreProductAPIApi.md#getproducts) | **GET** /products | 

<a name="addproduct"></a>
# **AddProduct**
> void AddProduct (ProductDto body)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AddProductExample
    {
        public void main()
        {
            var apiInstance = new BookStoreProductAPIApi();
            var body = new ProductDto(); // ProductDto | 

            try
            {
                apiInstance.AddProduct(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BookStoreProductAPIApi.AddProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ProductDto**](ProductDto.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getproduct"></a>
# **GetProduct**
> ProductDto GetProduct (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetProductExample
    {
        public void main()
        {
            var apiInstance = new BookStoreProductAPIApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                ProductDto result = apiInstance.GetProduct(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BookStoreProductAPIApi.GetProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 

### Return type

[**ProductDto**](ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getproducts"></a>
# **GetProducts**
> List<ProductDto> GetProducts ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetProductsExample
    {
        public void main()
        {
            var apiInstance = new BookStoreProductAPIApi();

            try
            {
                List&lt;ProductDto&gt; result = apiInstance.GetProducts();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BookStoreProductAPIApi.GetProducts: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ProductDto>**](ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
