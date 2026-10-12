//
import Kid from '../assets/Kid.webp'
//
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Chip from '@mui/material/Chip';
import Stack from '@mui/material/Stack';

//
import ArrowOutwardIcon from '@mui/icons-material/ArrowOutward';
import ShoppingBasketIcon from '@mui/icons-material/ShoppingBasket';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
//
function ProductSection(){
    return(
        <>
        <div className="py-4">
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                <Card sx={{ maxWidth: 345, width: "100%" }}>
                    <div className="relative">
                        {/* Overlay: tags and actions */}
                        <div className="absolute inset-0 z-10 flex justify-between items-start p-2 pointer-events-none">
                        {/* Tag */}
                        <Chip
                            color="primary"
                            label="NEW"
                            size="small"
                            sx={{ pointerEvents: "auto" }}
                        />

                        {/* Actions */}
                        <Stack
                            direction="column"
                            spacing={1}
                            sx={{
                            padding: "2px",
                            backgroundColor: "white",
                            borderRadius: "24px",
                            justifyContent: "center",
                            alignItems: "center",
                            pointerEvents: "auto",
                            }}
                        >
                            <IconButton aria-label="go to page" size="small">
                                <ArrowOutwardIcon 
                                fontSize="inherit" 
                                sx={{
                                    transition: "transform 0.3s ease",
                                    "&:hover": {
                                    transform: "rotate(45deg)",
                                    },
                                }}/>
                            </IconButton>

                            <IconButton aria-label="cart" size="small">
                                <ShoppingBasketIcon fontSize="inherit" />
                            </IconButton>

                            <IconButton aria-label="like" size="small">
                                <FavoriteBorderIcon fontSize="inherit" />
                            </IconButton>
                        </Stack>
                        </div>

                        {/* Product image */}
                        <CardMedia
                        component="img"
                        image={Kid}
                        alt="product img"
                        sx={{
                            height: 250,
                            width: "100%",
                            objectFit: "cover",
                        }}
                        />
                    </div>

                    <CardContent>
                        <Typography variant="h5" sx={{ color: "text.secondary" }}>
                        Áo Polo Dài Tay Na
                        </Typography>

                        <Typography variant="body2" sx={{ color: "text.secondary" }}>
                        990,000đ
                        </Typography>
                    </CardContent>
                </Card>
            </div>
        </div>
        </>
    )
}

export default ProductSection;