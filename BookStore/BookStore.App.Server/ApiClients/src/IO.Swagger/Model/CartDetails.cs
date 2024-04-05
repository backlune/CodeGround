/* 
 * BookStore.CartApi
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// CartDetails
    /// </summary>
    [DataContract]
        public partial class CartDetails :  IEquatable<CartDetails>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartDetails" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="cartHeaderId">cartHeaderId.</param>
        /// <param name="cartHeader">cartHeader.</param>
        /// <param name="productId">productId.</param>
        /// <param name="product">product.</param>
        /// <param name="count">count.</param>
        public CartDetails(Guid? id = default(Guid?), Guid? cartHeaderId = default(Guid?), CartHeader cartHeader = default(CartHeader), Guid? productId = default(Guid?), Product product = default(Product), int? count = default(int?))
        {
            this.Id = id;
            this.CartHeaderId = cartHeaderId;
            this.CartHeader = cartHeader;
            this.ProductId = productId;
            this.Product = product;
            this.Count = count;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or Sets CartHeaderId
        /// </summary>
        [DataMember(Name="cartHeaderId", EmitDefaultValue=false)]
        public Guid? CartHeaderId { get; set; }

        /// <summary>
        /// Gets or Sets CartHeader
        /// </summary>
        [DataMember(Name="cartHeader", EmitDefaultValue=false)]
        public CartHeader CartHeader { get; set; }

        /// <summary>
        /// Gets or Sets ProductId
        /// </summary>
        [DataMember(Name="productId", EmitDefaultValue=false)]
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Gets or Sets Product
        /// </summary>
        [DataMember(Name="product", EmitDefaultValue=false)]
        public Product Product { get; set; }

        /// <summary>
        /// Gets or Sets Count
        /// </summary>
        [DataMember(Name="count", EmitDefaultValue=false)]
        public int? Count { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CartDetails {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CartHeaderId: ").Append(CartHeaderId).Append("\n");
            sb.Append("  CartHeader: ").Append(CartHeader).Append("\n");
            sb.Append("  ProductId: ").Append(ProductId).Append("\n");
            sb.Append("  Product: ").Append(Product).Append("\n");
            sb.Append("  Count: ").Append(Count).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CartDetails);
        }

        /// <summary>
        /// Returns true if CartDetails instances are equal
        /// </summary>
        /// <param name="input">Instance of CartDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CartDetails input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CartHeaderId == input.CartHeaderId ||
                    (this.CartHeaderId != null &&
                    this.CartHeaderId.Equals(input.CartHeaderId))
                ) && 
                (
                    this.CartHeader == input.CartHeader ||
                    (this.CartHeader != null &&
                    this.CartHeader.Equals(input.CartHeader))
                ) && 
                (
                    this.ProductId == input.ProductId ||
                    (this.ProductId != null &&
                    this.ProductId.Equals(input.ProductId))
                ) && 
                (
                    this.Product == input.Product ||
                    (this.Product != null &&
                    this.Product.Equals(input.Product))
                ) && 
                (
                    this.Count == input.Count ||
                    (this.Count != null &&
                    this.Count.Equals(input.Count))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.CartHeaderId != null)
                    hashCode = hashCode * 59 + this.CartHeaderId.GetHashCode();
                if (this.CartHeader != null)
                    hashCode = hashCode * 59 + this.CartHeader.GetHashCode();
                if (this.ProductId != null)
                    hashCode = hashCode * 59 + this.ProductId.GetHashCode();
                if (this.Product != null)
                    hashCode = hashCode * 59 + this.Product.GetHashCode();
                if (this.Count != null)
                    hashCode = hashCode * 59 + this.Count.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
