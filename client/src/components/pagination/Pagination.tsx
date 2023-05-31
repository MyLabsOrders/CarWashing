import { useEffect, useState } from "react";
import ProductsContainer from "../containers/ItemContainer";
import { IProduct, createProduct, createProducts } from "../forms/ProductForm/Product";
import { getAllProducts } from "../../lib/products/products";

import DP002 from "../../assets/data_washing/DP002.jpg";
import FX1914BPL from "../../assets/data_washing/FX 1914BPL.jpg";
import HR3500 from "../../assets/data_washing/HR 3500.jpg";
import MV925 from "../../assets/data_washing/MV925.jpg";
import PAT46 from "../../assets/data_washing/PA T46.jpg";
import Panda423 from "../../assets/data_washing/Panda423.jpg";
import PROCARLT from "../../assets/data_washing/PROCAR LT.jpg";
import TR02 from "../../assets/data_washing/TR-02.jpg";
import VIKAN290 from "../../assets/data_washing/VIKAN 290.jpg";
import D50 from "../../assets/data_washing/Д50.jpg";

const defaultProducts = (): IProduct[] => {
    const products: IProduct[] = [
        createProduct("0", "DP002", 28190, "available", DP002),
        createProduct("1", "FX 1914BPL", 95470, "available", FX1914BPL),
        createProduct("2", "HR 3500", 29000, "available", HR3500),
        createProduct("3", "MV925", 4975, "available", MV925),
        createProduct("4", "PA T46", 20260, "available", PAT46),
        createProduct("5", "Panda423", 63150, "available", Panda423),
        createProduct("6", "PROCAR LT", 31820, "available", PROCARLT),
        createProduct("7", "TR-02", 4920, "available", TR02),
        createProduct("8", "VIKAN 290", 1960, "available", VIKAN290),
        createProduct("9", "Д50", 9225, "available", D50),
    ];
    return products;
};

export interface IPaginationProps {
    apiUrl: string;
}

const Pagination = ({ apiUrl }: IPaginationProps) => {
    const [products, setProducts] = useState<IProduct[]>(defaultProducts());
    // const [products, setProducts] = useState<IProduct[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [fetching, setFetching] = useState(false);

    useEffect(() => {
        if (fetching) {
            fetchProducts();
        }
    }, []);

    const fetchProducts = async () => {
        try {
            const { data } = await getAllProducts(currentPage);
            setProducts([
                ...products, //Remain already fetched products
                ...data.orders.filter((order) => !order.orderDate), //+ append orders without orderDate
            ]);
            setCurrentPage((prev) => prev + 1);
        } catch (error) {}
    };

    useEffect(() => {
        document.addEventListener("scroll", handleScroll);

        return function () {
            document.removeEventListener("scroll", handleScroll);
        };
    }, []);

    const handleScroll = (e: any) => {
        if (
            e.target.documentElement.scrollHeight -
                (e.target.documentElement.scrollTop + window.innerHeight) <
            100
        ) {
            setFetching(true);
        }
    };

    return (
        <>
            <ProductsContainer products={products} />
        </>
    );
};

export default Pagination;

