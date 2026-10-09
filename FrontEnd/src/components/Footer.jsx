
import ImageICON from '../assets/Icon.png'
//
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
//
import ArrowRightOutlinedIcon from '@mui/icons-material/ArrowRightOutlined';
import SendIcon from '@mui/icons-material/Send';
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';
import HeadphonesOutlinedIcon from '@mui/icons-material/HeadphonesOutlined';
import MailOutlinedIcon from '@mui/icons-material/MailOutlined';
//
function Footer(){
    return(
        <>
        <footer className="p-4 bg-[#20716A]">
            <div className="p-2 flex flex-col items-center gap-2 text-white">
                <p className="text-xl">ĐĂNG KÝ NHẬN TIN</p>
                <p className="text-sm">Để cập nhật những sản phẩm mới, nhận thông tin ưu đãi đặc biệt và thông tin giảm giá khác.</p>
                <div className="w-full flex items-center justify-center">
                    <input type="text" className="bg-white p-3 w-3/4 md:w-1/2 lg:w-1/3 rounded-sm"
                    placeholder="Enter Ur Email"/>
                    <Button variant="contained" 
                    sx={{padding: '12px', 
                        backgroundColor: '#FFC0C2',
                        borderRadius: '4px'}}
                    endIcon={<SendIcon />}>
                        Send
                    </Button>
                </div>
            </div>
            <Divider/>
            <div className="p-2 w-4/5 flex justify-start md:justify-center">
                <div className="
                    grid-cols-1 md:gap-4
                    grid md:grid-cols-3 
                    gap-8 text-white">
                    <div className="">
                        <p className='font-bold text-xl'>Về KINGDOM:</p>
                        <img src={ImageICON} alt="Web-icon" />
                    </div>   
                    <div className="">
                        <p className='font-bold text-xl'>LIÊN HỆ HỢP TÁC</p>
                        <List>
                            <ListItem disablePadding sx={{display: 'flex',flexDirection: 'column',alignItems: 'stretch' }}>
                                <ListItemButton>
                                    <ListItemIcon>
                                        <HomeOutlinedIcon sx={{color: 'white'}}/>
                                    </ListItemIcon>
                                    <ListItemText primary="home" />
                                </ListItemButton>  
                                <ListItemButton>
                                    <ListItemIcon>
                                        <HeadphonesOutlinedIcon sx={{color: 'white'}}/>
                                    </ListItemIcon>
                                    <ListItemText primary="0900" />
                                </ListItemButton>  
                                <ListItemButton>
                                    <ListItemIcon>
                                        <MailOutlinedIcon sx={{color: 'white'}}/>
                                    </ListItemIcon>
                                    <ListItemText primary=": test@gmail" />
                                </ListItemButton>  
                            </ListItem>
                        </List>
                    </div>
                    <div className="">
                        <p className='font-bold text-xl'>Hỗ trợ - chính sách:</p>
                        <ul>
                            <li><ArrowRightOutlinedIcon/>Giới thiệu</li>
                            <li><ArrowRightOutlinedIcon/>Liên hệ</li>
                            <li><ArrowRightOutlinedIcon/>Hệ thống cửa hàng</li>
                            <li><ArrowRightOutlinedIcon/>Phương thức thanh toán</li>
                        </ul>
                    </div>
                </div>   
            </div>             
        </footer>
        </>
    )
}

export default Footer;