import { Button, Box, Modal, FormControl, TextField } from "@mui/material"
import { green, grey } from "@mui/material/colors"
import { useState } from "react"
import { createProduct } from "../../../lib/products/products"
import { getCookie } from "typescript-cookie"
import { useNavigate } from "react-router"
import { convertToBase64 } from "../../utils/utils"

const CreateProductForm = () => {
	const [isModaOpen, setIsModalOpen] = useState(false)
	const [status, setStatus] = useState('')
	const [total, setTotal] = useState(0)
	const [name, setName] = useState('')
	const [image, setImage] = useState<File | null>(null)
	const navigate = useNavigate()

	const handleOpen = () => {
		setIsModalOpen(true)
	}

	const handleClose = () => {
		setIsModalOpen(false)
	}

	const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		if (e.target.files && e.target.files.length > 0) {
			setImage(e.target.files[0]);
		}
	}

	const handleClick = async () => {
		setIsModalOpen(false);

		try {
			const convertedImage = await convertToBase64(image);
			await createProduct(
				getCookie('jwt-authorization') ?? '',
				{ name: name, status: status, total: total, orderImage: convertedImage })
		} catch (error: any) {
			navigate("/profile", { state: { message: `${error?.message}`, type: "error" } })

		}
	}

	return (
		<Box>
			<Button
				onClick={handleOpen}
				sx={{
					"&:hover": {
						backgroundColor: green[500],
						color: "white",
					}
				}}>
				Create product
			</Button>
			<Modal open={isModaOpen} onClose={handleClose} sx={{ backdropFilter: "blur(5px)" }}>
				<Box
					sx={{
						position: "absolute",
						top: "50%",
						left: "50%",
						transform: "translate(-50%, -50%)",
						width: 400,
						bgcolor: "background.paper",
						boxShadow: 24,
						p: 4,
						display: 'flex',
						justifyContent: 'center',
						alignItems: 'center',
						borderRadius: '10px',
						backgroundColor: green[800]
					}}
				>
					<FormControl>
						<TextField value={status} onChange={(e) => setStatus(e.target.value)} label="Status ( Available/Rented ) " fullWidth />
						<TextField value={name} onChange={(e) => setName(e.target.value)} label="Name" fullWidth />
						<TextField value={total} onChange={(e) => setTotal(e.target.value as unknown as number)} label="Total" fullWidth type="number" />
						<input
							type="file"
							accept="image/*"
							onChange={handleImageChange}
						/>
						<Button
							onClick={handleClick}
							sx={{
								"&:hover": {
									backgroundColor: green[500],
									color: "white",
								},
								backgroundColor: '#132f4b'
							}}>
							Create
						</Button>
					</FormControl>
				</Box>
			</Modal>
		</Box>
	)
}

export default CreateProductForm