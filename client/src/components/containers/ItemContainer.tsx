import { Box, Container, Grid } from "@mui/material";
import { Product } from "../forms";
import { IProduct } from "../forms/ProductForm/Product";
import { green } from "@mui/material/colors";

interface ProductContainerProps {
	products: IProduct[]
}

const ProductsContainer = ({ products }: ProductContainerProps) => {
	return (
		<Container sx={{ display: "flex", justifyContent: "center" }}>
			<Grid container spacing={2} sx={{ marginTop: "80px" }} >
				{products.map((product) => (
					<Grid key={product.id} item md={6} height={"8rem"} sx={{ marginBottom: "20px" }}>
						<Box
							height={"100%"}
							sx={{
								display: "flex",
								flexDirection: "row",
								justifyContent: "space-between",
								backgroundColor: green[300],
								padding: "10px",
								borderRadius: "4px",
								boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
							}}>
							<Product
								id={product.id}
								image={product.image}
								name={product.name}
								total={product.total}
								status={product.status}
							/>
						</Box>
					</Grid>
				))}
			</Grid>
		</Container>
	);
};

export default ProductsContainer;