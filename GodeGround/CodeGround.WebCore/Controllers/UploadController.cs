using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;
using System.Text;
using NSwag.Annotations;
using NSwag.AspNetCore;


namespace CodeGround.WebCore.Controllers
{
   [Route("api/[controller]")]
   public class UploadController : Controller
   {
      private static readonly FormOptions _defaultFormOptions = new FormOptions();


      // GET api/base/5
      [HttpGet("{id}")]
      public string Get(int id)
      {
         return "hello";
      }

      // POST api/values
     [HttpPost("uploadFile")]
     [DisableFormValueModelBinding]
     [SwaggerResponse(typeof(string))]
     [MultiFormFileDataAttribute(Name = "test")]
      //[SwaggerOperationProcessor(typeof(MyOperationProcessor))]
      public async Task<IActionResult> UploadFile()
      {
         if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
         {
            return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
         }

         List<string> uploadedFiles = new List<string>();
         // Used to accumulate all the form url encoded key value pairs in the 
         // request.
         string targetFilePath = null;

         var boundary = MultipartRequestHelper.GetBoundary(
             MediaTypeHeaderValue.Parse(Request.ContentType),
             _defaultFormOptions.MultipartBoundaryLengthLimit);
         var reader = new MultipartReader(boundary, HttpContext.Request.Body);

         var section = await reader.ReadNextSectionAsync();
         while (section != null)
         {
            ContentDispositionHeaderValue contentDisposition;
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

            if (hasContentDispositionHeader)
            {
               if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
               {
                  targetFilePath = Path.GetTempFileName();
                  using (var targetStream = System.IO.File.Create(targetFilePath))
                  {
                     await section.Body.CopyToAsync(targetStream);

                     //_logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                     uploadedFiles.Add($"Copied the uploaded file '{targetFilePath}'");
                  }
               }
            }

            // Drains any remaining section body that has not been consumed and
            // reads the headers for the next section.
            section = await reader.ReadNextSectionAsync();
         }
         return Content(string.Join(Environment.NewLine, uploadedFiles));
      }


      // POST api/values
      //[HttpPost("upload")]
      //[DisableFormValueModelBinding]
      //public async Task<IActionResult> Upload()
      //{

      //   if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
      //   {
      //      return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
      //   }

      //   // Used to accumulate all the form url encoded key value pairs in the 
      //   // request.
      //   var formAccumulator = new KeyValueAccumulator();
      //   string targetFilePath = null;

      //   var boundary = MultipartRequestHelper.GetBoundary(
      //       MediaTypeHeaderValue.Parse(Request.ContentType),
      //       _defaultFormOptions.MultipartBoundaryLengthLimit);
      //   var reader = new MultipartReader(boundary, HttpContext.Request.Body);

      //   var section = await reader.ReadNextSectionAsync();
      //   while (section != null)
      //   {
      //      ContentDispositionHeaderValue contentDisposition;
      //      var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

      //      if (hasContentDispositionHeader)
      //      {
      //         if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
      //         {
      //            targetFilePath = Path.GetTempFileName();
      //            using (var targetStream = System.IO.File.Create(targetFilePath))
      //            {
      //               await section.Body.CopyToAsync(targetStream);

      //               //_logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
      //            }
      //         }
      //         else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
      //         {
      //            // Content-Disposition: form-data; name="key"
      //            //
      //            // value

      //            // Do not limit the key name length here because the 
      //            // multipart headers length limit is already in effect.
      //            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
      //            var encoding = GetEncoding(section);
      //            using (var streamReader = new StreamReader(
      //                section.Body,
      //                encoding,
      //                detectEncodingFromByteOrderMarks: true,
      //                bufferSize: 1024,
      //                leaveOpen: true))
      //            {
      //               // The value length limit is enforced by MultipartBodyLengthLimit
      //               var value = await streamReader.ReadToEndAsync();
      //               if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
      //               {
      //                  value = String.Empty;
      //               }
      //               formAccumulator.Append(key, value);

      //               if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
      //               {
      //                  throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
      //               }
      //            }
      //         }
      //      }

      //      // Drains any remaining section body that has not been consumed and
      //      // reads the headers for the next section.
      //      section = await reader.ReadNextSectionAsync();
      //   }

      //   // Bind form data to a model
      //   //var user = new User();
      //   //var formValueProvider = new FormValueProvider(
      //   //    BindingSource.Form,
      //   //    new FormCollection(formAccumulator.GetResults()),
      //   //    CultureInfo.CurrentCulture);

      //   //var bindingSuccessful = await TryUpdateModelAsync(user, prefix: "",
      //   //    valueProvider: formValueProvider);
      //   //if (!bindingSuccessful)
      //   //{
      //   //   if (!ModelState.IsValid)
      //   //   {
      //   //      return BadRequest(ModelState);
      //   //   }
      //   //}

      //   //var uploadedData = new UploadedData()
      //   //{
      //   //   Name = user.Name,
      //   //   Age = user.Age,
      //   //   Zipcode = user.Zipcode,
      //   //   FilePath = targetFilePath
      //   //};
      //   return Content("Success!");
      //}

      private static Encoding GetEncoding(MultipartSection section)
      {
         MediaTypeHeaderValue mediaType;
         var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
         // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
         // most cases.
         if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
         {
            return Encoding.UTF8;
         }
         return mediaType.Encoding;
      }
   }


   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
   {
      public void OnResourceExecuting(ResourceExecutingContext context)
      {
         var formValueProviderFactory = context.ValueProviderFactories
             .OfType<FormValueProviderFactory>()
             .FirstOrDefault();
         if (formValueProviderFactory != null)
         {
            context.ValueProviderFactories.Remove(formValueProviderFactory);
         }

         var jqueryFormValueProviderFactory = context.ValueProviderFactories
             .OfType<JQueryFormValueProviderFactory>()
             .FirstOrDefault();
         if (jqueryFormValueProviderFactory != null)
         {
            context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
         }
      }

      public void OnResourceExecuted(ResourceExecutedContext context)
      {
      }
   }

   public static class MultipartRequestHelper
   {
      // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
      // The spec says 70 characters is a reasonable limit.
      public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
      {
         var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
         if (string.IsNullOrWhiteSpace(boundary))
         {
            throw new InvalidOperationException("Missing content-type boundary.");
         }

         if (boundary.Length > lengthLimit)
         {
            throw new InvalidOperationException(
                $"Multipart boundary length limit {lengthLimit} exceeded.");
         }

         return boundary;
      }

      public static bool IsMultipartContentType(string contentType)
      {
         return !string.IsNullOrEmpty(contentType)
                && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
      }

      public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
      {
         // Content-Disposition: form-data; name="key";
         return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && string.IsNullOrEmpty(contentDisposition.FileName)
                && string.IsNullOrEmpty(contentDisposition.FileNameStar);
      }

      public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
      {
         // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
         return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && (!string.IsNullOrEmpty(contentDisposition.FileName)
                    || !string.IsNullOrEmpty(contentDisposition.FileNameStar));
      }
   }
}

