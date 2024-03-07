import React from "react";
import { useEffect, useState } from "react";

export interface Product {
  name: string;
  price: number;
  description: string;
  id: string;
}

export default function ProductView() {
  const [products, setProducts] = useState<Product[]>();

  const populateProduts = async () => {
    const response = await fetch("/products");
    const data = await response.json();
    setProducts(data);
  };

  const contents =
    products === undefined ? (
      <p>
        <em>
          Loading... Please refresh once the ASP.NET backend has started. See{" "}
          <a href="https://aka.ms/jspsintegrationreact">
            https://aka.ms/jspsintegrationreact
          </a>{" "}
          for more details.
        </em>
      </p>
    ) : (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Description</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.name}>
              <td>{product.price}</td>
              <td>{product.description}</td>
              <td>
                <button
                  type="button"
                  className="btn btn-outline-danger"
                  onClick={() => deleteProduct(product.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );

  const deleteProduct = async (productId: string) => {
    await fetch("/products/" + productId, {
      method: "DELETE",
    });
    await populateProduts();
  };

  useEffect(() => {
    populateProduts();
  }, []);

  return (
    <div>
      <h1 id="tabelLabel">Products</h1>
      <p>Available products</p>
      {contents}
    </div>
  );
}
