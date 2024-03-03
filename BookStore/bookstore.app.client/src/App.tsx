import { useEffect, useState } from 'react';
import './App.scss';



interface Product {
    name: string;
    price: number;
    description: string;
    id: string;
}

function App() {
    const [products, setProducts] = useState<Product[]>();

    useEffect(() => {
        populateProduts();
    }, []);

    const contents = products === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Description</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {products.map(product =>
                    <tr key={product.name}>
                        <td>{product.price}</td>
                        <td>{product.description}</td>
                        <td><button type="button" class="btn btn-outline-danger" onClick={() => deleteProduct(product.id)} >Delete</button></td>
                    </tr>
                )}
            </tbody>
        </table>

    return (
        <div>
            <h1 id="tabelLabel">Products</h1>
            <p>Available products</p>
            {contents}
        </div>
    );

    async function populateProduts() {
        const response = await fetch('products');
        const data = await response.json();
        setProducts(data);
    }

    async function deleteProduct(productId: string) {
        await fetch("products/" + productId, {
            method: "DELETE"
        })
        await populateProduts();
    }
}

export default App;