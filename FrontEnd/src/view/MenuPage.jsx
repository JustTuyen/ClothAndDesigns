import * as React from 'react';
//
import Kid from '../assets/Kid.webp'

//
import NavBar from "../components/NavBar";
import Footer from "../components/Footer";
//
import Typography from '@mui/material/Typography';
import Stack from '@mui/material/Stack';
import Divider from "@mui/material/Divider";
import Button from '@mui/material/Button';
import MenuIcon from '@mui/icons-material/Menu';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import IconButton from '@mui/material/IconButton';
import Chip from '@mui/material/Chip';
import CardContent from '@mui/material/CardContent';
import Slider from '@mui/material/Slider';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import ListItemText from '@mui/material/ListItemText';
import ExpandLess from '@mui/icons-material/ExpandLess';
import ExpandMore from '@mui/icons-material/ExpandMore';
import ListItemButton from '@mui/material/ListItemButton';
import List from '@mui/material/List';
import Collapse from '@mui/material/Collapse';





//
import ClearIcon from '@mui/icons-material/Clear';
import ArrowOutwardIcon from '@mui/icons-material/ArrowOutward';
import ShoppingBasketIcon from '@mui/icons-material/ShoppingBasket';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
//
const formatPrice = (price) =>
    `${price.toLocaleString("vi-VN")}₫`;

function Menu(){
    const [open, setOpen] = React.useState(false);
    const toggleDrawer = (newOpen) => () => {
        setOpen(newOpen);
    };

    const [value, setValue] = React.useState([200000, 2000000]);
    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const [openCat, setOpenCat] = React.useState(true);

    const handleClickCat = () => {
        setOpenCat(!openCat);
    };

    const DrawerList = (
        <Box sx={{ width: { xs: '100vw', sm: 500 }, 
        height: '100%', display: 'flex', flexDirection: 'column' }} 
        role="presentation">
            <div className='flex justify-between items-center p-4'>
                <Typography variant="h5">FILTER</Typography>
                <IconButton aria-label="Delete item" color="default" onClick={toggleDrawer(false)}>
                    <ClearIcon fontSize="large" />
                </IconButton>
            </div>
            <Divider/>
            <Box sx={{ width: '100%', p: 4, gap: { xs: 1, md: 2 },}}>
                <Typography variant="body1">Lọc theo giá</Typography>
                <Slider
                    getAriaLabel={() => "Price range"}
                value={value}
                onChange={handleChange}
                valueLabelDisplay="auto"
                valueLabelFormat={formatPrice}
                getAriaValueText={formatPrice}
                min={0}
                max={5000000}
                step={50000}
                />
                <Typography variant="body2" gutterBottom>
                    Giá: {formatPrice(value[0])} – {formatPrice(value[1])}
                </Typography>
                <Button variant="contained" sx={{width: '100%'}}>Filter!</Button>
                <Typography
                component="button"
                sx={{
                    display: "block",
                    ml: "auto",
                    mt: 1,
                    border: 0,
                    background: "none",
                    cursor: "pointer",
                    textAlign: "right",
                    color: "#C5C1C1",
                    "&:hover": { color: "text.primary" },
                }}
                >
                Clear filter
            </Typography>
            </Box>
            <Divider variant="middle" />
            <Box sx={{ width: '100%', p: 4}}>
                <Typography variant="body1">Danh mục sản phẩm</Typography>
                <List>
                    <ListItemButton onClick={handleClickCat}>
                        <ListItemText primary="Inbox" />
                        {openCat ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                </List>
                <Collapse in={openCat} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        <ListItemButton sx={{ pl: 4 }}>
                            <ListItemText primary="Starred" />
                        </ListItemButton>
                    </List>
                </Collapse>
            </Box>
            <Divider variant="middle" />
            <Box sx={{ width: '100%', p: 4}}>
                <Typography variant="body1">Tình trạng kho</Typography>
                <FormGroup>
                    <FormControlLabel control={<Checkbox defaultChecked />} label="Giảm gía" />
                    <FormControlLabel control={<Checkbox />} label="Hàng mới về" />
                </FormGroup>
            </Box>
        </Box>
    );


    
    return(
        <>
        <NavBar/>
        <div className="w-full min-h-screen bg-white">
            <section className="bg-amber-50 flex flex-col gap-8 w-4/5 mx-auto p-4">
                <Stack direction="row" 
                sx={{
                alignItems: "center",
                gap: { xs: 1, md: 2 },
                }}>
                    <Typography variant="h4">
                        thoi-trang-nam
                    </Typography>
                    <Typography variant="caption">
                        ( 47 kết quả )
                    </Typography>
                </Stack>
                <Divider/>
                <Stack direction="row" 
                sx={{
                    alignItems: "center",
                    justifyContent: "space-between",
                    width: "100%",
                    gap: { xs: 1, md: 2 },
                }}>
                    <Button sx={{color:'black'}} startIcon={<MenuIcon />} onClick={toggleDrawer(true)}>
                        Filter
                    </Button>
                    <Drawer open={open} onClose={toggleDrawer(false)}>
                        {DrawerList}
                    </Drawer>

                    <div className="border p-2 rounded-full">
                        <select name="" id="">
                            <option value="">Top to Bottom</option>
                            <option value="">This</option>
                        </select>
                    </div>
                </Stack>
                <div className="grid grid-cols-2 md:grid-cols-3  lg:grid-cols-5 gap-4">
                     <Card sx={{ maxWidth: 345, width: "100%", }}  className="slide-in-card">
                        <div className="relative">
                            {/* Overlay: tags and actions */}
                            <div className="absolute inset-0 z-10 flex justify-between items-start p-2 pointer-events-none">
                            {/* Tag */}
                                <Stack
                                    direction="column"
                                    spacing={1}
                                    sx={{
                                    justifyContent: "center",
                                    alignItems: "center",
                                    pointerEvents: "auto",
                                    }}
                                >
                                <Chip
                                    color="primary"
                                    label="NEW"
                                    size="small"
                                    sx={{ pointerEvents: "auto" }}
                                />
                                <Chip
                                    color="error"
                                    label="-40%"
                                    size="small"
                                    sx={{ pointerEvents: "auto" }}
                                />
                                </Stack>
                        

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
            </section>
        </div>
        <Footer/>
        </>
    )
}

export default Menu;