import * as React from 'react';
//
import ImgIcon from '../assets/Icon.png'
import Kid from '../assets/Kid.webp'

//
import Stack from '@mui/material/Stack';
import IconButton from '@mui/material/IconButton';
import Badge, { badgeClasses } from '@mui/material/Badge';
import { styled } from '@mui/material/styles';
import Tooltip from '@mui/material/Tooltip';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';

//
import ClearIcon from '@mui/icons-material/Clear';
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import AccountCircleOutlinedIcon from '@mui/icons-material/AccountCircleOutlined';
import ShoppingBasketOutlinedIcon from '@mui/icons-material/ShoppingBasketOutlined';
import MenuIcon from '@mui/icons-material/Menu';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
//
const CartBadge = styled(Badge)`
  & .${badgeClasses.badge} {
    top: -12px;
    right: -6px;
  }
`;



function NavBar(){

    //drawer for cart
    const [openCart, setOpenCart] = React.useState(false);
    const toggleCart = (newOpen) => () => {
        setOpenCart(newOpen);
    };

    const CartList = (
        <Box sx={{ width: { xs: '100vw', sm: 500 }, height: '100%', display: 'flex', flexDirection: 'column' }} role="presentation">
            <div className='flex justify-between items-center p-4'>
                <Typography variant="h5">Cart</Typography>
                <IconButton aria-label="Delete item" color="default" onClick={toggleCart(false)}>
                    <ClearIcon fontSize="large" />
                </IconButton>
            </div>
            <Divider />
            <Stack spacing={1} sx={{ p: 2, flex: 1, overflowY: 'auto' }}>
                <Card  sx={{ display: 'flex',alignItems: "stretch",  width: "100%",}}>
                    <CardMedia
                        component="img"
                        sx={{width: { xs: 100, sm: 140 }, objectFit: 'cover', flexShrink: 0, }}
                        image={Kid}
                        alt="cart items placeholder"
                    />
                    <Box sx={{ display: 'flex', flexDirection: 'column', p: { xs: 1, sm: 2 }, flex: 1, minWidth: 0,gap: 1, }}>
                        <Typography component="div" variant="body1" 
                        sx={{
                            fontWeight: 500,
                            display: "-webkit-box",
                            WebkitLineClamp: 2,
                            WebkitBoxOrient: "vertical",
                            overflow: "hidden",
                        }}>
                            Áo Thun Airy Tee 88711 – Áo Thun Nam Form Relax Thoáng Mát
                        </Typography>
                        <Typography component="div" variant="body2" color="text.secondary">
                            GREIGE / M
                        </Typography>
                        <div className="grid grid-cols-2 gap-2 items-center">
                            <Box sx={{
                                display: "flex",
                                alignItems: "center",
                                border: "1px solid",
                                borderColor: "divider",
                                borderRadius: 5,
                                }}>
                                <IconButton aria-label="minus">
                                    <RemoveIcon fontSize="small"/>
                                </IconButton>
                                <input type='number' value={1}
                                className="w-12 text-center rounded"/>
                                <IconButton aria-label="add">
                                    <AddIcon fontSize="small"/>
                                </IconButton>
                            </Box>
                            <Typography sx={{ fontWeight: 600, whiteSpace: "nowrap" }} variant="body1">
                                360,000₫
                            </Typography>
                        </div>
                    </Box>
                   <Box sx={{ p: { xs: 0.25, sm: 1 }, flexShrink: 0 }}>
                        <IconButton aria-label="Delete item" color="default">
                            <HighlightOffIcon fontSize="small" />
                        </IconButton>
                    </Box>
                </Card>
            </Stack>
            <Divider />
            <Box sx={{ p: 2 }}>
                <Typography variant="h6" gutterBottom>TỔNG TIỀN: 300.000</Typography>
                <Button fullWidth variant="contained" sx={{ bgcolor: '#DF301C', '&:hover': { bgcolor: '#c42a18' } }}>
                    Mua Ngay
                </Button>
                <Typography variant="caption" gutterBottom>Xem giỏ hàng</Typography>  
            </Box>
        </Box>
    );
    //drawer for side menu
    const [openDrawer, setOpenDrawer] = React.useState(false);

    const toggleDrawer = (newOpen) => () => {
        setOpenDrawer(newOpen);
    };
    const DrawerList = (
        <Box sx={{ width: 250, }} role="presentation" onClick={toggleDrawer(false)}>
            <List>
                {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
                <ListItem key={text} disablePadding>
                    <ListItemButton>
                    <ListItemIcon>
                        {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                    </ListItemButton>
                </ListItem>
                ))}
            </List>
            <Divider />
            <List>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                <ListItem key={text} disablePadding>
                    <ListItemButton>
                    <ListItemIcon>
                        {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                    </ListItemButton>
                </ListItem>
                ))}
            </List>
        </Box>
    );
    //
    const id = React.useId();
    const buttonId = `${id}-button`;
    const menuId = `${id}-menu`;

    //profile btn
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };



    return (
        <>
        <nav className="sticky top-0 z-50 p-2 bg-[#00B7CD]">
            <Stack direction="row" 
            sx={{
            alignItems: "center",
            width: "100%",
            gap: { xs: 1, md: 2 },
            }}>
                <div className="p-2 block lg:hidden">
                    <IconButton aria-label="drawer" onClick={toggleDrawer(true)}>
                        <MenuIcon />
                    </IconButton>
                    <Drawer open={openDrawer} onClose={toggleDrawer(false)}>
                        {DrawerList}
                    </Drawer>
                </div>
                <img src={ImgIcon} alt="wed-logo" className="w-14 sm:w-20 shrink-0"/>
                <Stack direction="row" spacing={1} 
                sx={{
                    alignItems: "center",
                    flex: 1,
                    justifyContent: "center",
                    display: { xs: "none", lg: "flex" },
                }}>
                    <Button size='small'  sx={{color: 'white', textTransform: 'uppercase',
                        fontWeight: 'bold'}} endIcon={<ArrowDropDownIcon />}>
                        MEN COLLECTION
                    </Button>
                        <Button size='small' sx={{color: 'white', textTransform: 'uppercase',
                            fontWeight: 'bold'}} endIcon={<ArrowDropDownIcon />}>
                        Woman Collections
                    </Button>
                        <Button size='small'  sx={{color: 'white', textTransform: 'uppercase',
                        fontWeight: 'bold'}} endIcon={<ArrowDropDownIcon />}>
                        KID COLLECTION
                    </Button>
                        <Button size='small'  sx={{color: 'white', textTransform: 'uppercase',
                        fontWeight: 'bold'}} endIcon={<ArrowDropDownIcon />}>
                        ACCESSORIES
                    </Button>
                </Stack>
                <Stack direction="row" spacing={{ xs: 0, sm: 1 }}
                sx={{
                    alignItems: "center",
                    marginLeft: { xs: "auto", lg: 0 },
                    flexShrink: 0,
                }}>
                <Tooltip title="Click to search">
                        <IconButton aria-label="search" sx={{color:'white'}}>
                            <SearchOutlinedIcon />
                        </IconButton>
                    </Tooltip>
                    <IconButton aria-label="cart" onClick={toggleCart(true)}>
                        <ShoppingBasketOutlinedIcon  sx={{color:'white'}} fontSize="small" />
                        <CartBadge badgeContent={2} 
                        color="error"
                        overlap="circular" />
                    </IconButton>
                    <Drawer open={openCart} anchor='right' onClose={toggleCart(false)}>
                        {CartList}
                    </Drawer>
                    <Tooltip title="Click to see profile">
                        <IconButton aria-label="user" sx={{color:'white'}}
                            id={buttonId}
                            aria-controls={open ? menuId : undefined}
                            aria-haspopup="true"
                            aria-expanded={open}
                            onClick={handleClick}
                        >
                            <AccountCircleOutlinedIcon />
                        </IconButton>
                        <Menu
                            id={menuId}
                            anchorEl={anchorEl}
                            open={open}
                            onClose={handleClose}
                            slotProps={{
                                list: {
                                    'aria-labelledby': buttonId,
                                },
                            }}
                        >
                            <MenuItem onClick={handleClose}>Profile</MenuItem>
                            <MenuItem onClick={handleClose}>My account</MenuItem>
                            <MenuItem onClick={handleClose}>Logout</MenuItem>
                        </Menu>
                    </Tooltip>
                </Stack>
            </Stack>
        </nav>
        </>
    )
}

export default NavBar;