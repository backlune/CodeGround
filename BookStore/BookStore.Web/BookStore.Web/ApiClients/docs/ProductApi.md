# IO.Swagger.Api.ProductApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddProduct**](ProductApi.md#addproduct) | **POST** /api/products | 
[**DeleteProduct**](ProductApi.md#deleteproduct) | **DELETE** /api/products/{id} | 
[**GetProduct**](ProductApi.md#getproduct) | **GET** /api/products/{id} | 
[**GetProducts**](ProductApi.md#getproducts) | **GET** /api/products | 

<a name="addproduct"></a>
# **AddProduct**
> ProductDto AddProduct (ProductDto body)



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
            var apiInstance = new ProductApi();
            var body = new ProductDto(); // ProductDto | 

            try
            {
                ProductDto result = apiInstance.AddProduct(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.AddProduct: " + e.Message );
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

[**ProductDto**](ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="deleteproduct"></a>
# **DeleteProduct**
> void DeleteProduct (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteProductExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.DeleteProduct(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.DeleteProduct: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
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
            var apiInstance = new ProductApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                ProductDto result = apiInstance.GetProduct(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.GetProduct: " + e.Message );
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
            var apiInstance = new ProductApi();

            try
            {
                List&lt;ProductDto&gt; result = apiInstance.GetProducts();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.GetProducts: " + e.Message );
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
