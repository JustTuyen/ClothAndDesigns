//
import ChevronRightIcon from '@mui/icons-material/ChevronRight';

//
import DetailTab from '../components/DetailTab.jsx';
import Navbar from '../components/NavBar.jsx'
import Footer from '../components/Footer.jsx'
import ImageGallery from '../components/ImageGallary.jsx';
//

import Rating from '@mui/material/Rating';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Button from '@mui/material/Button';
//
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';
import ArrowOutwardIcon from '@mui/icons-material/ArrowOutward';
//

function DetailPage(){
    return(
        <>
        <Navbar/>
        <div className="flex flex-col gap-2 min-h-screen w-full">
            
            {/* navigator */}
            <div className="flex gap-2 p-2 font-thin text-sm md:text-md 
            justify-start bg-[#FFF1D1]">
                <p>Home</p>
                <ChevronRightIcon/>
                <p>Category</p>
                <ChevronRightIcon/>
                <p>SubCategory</p>
                <ChevronRightIcon/>
                <p>Product Name</p>
            </div>

            {/* detail card */}
            <div className="grid grid-cols-1 md:grid-cols-2 p-4 gap-4">
                {/* images */}
                <div className="">
                    <ImageGallery/>
                </div>

                {/* product details */}
                <div className="flex flex-col gap-4 p-4">
                    
                    <div className="flex flex-wrap items-center justify-between gap-2">
                        <p>Loại sản phẩm: Áo Polo</p>
                        <Stack direction="row" spacing={1} alignItems="center">
                            <Rating name="size-small" defaultValue={2} size="small" />
                            <Typography variant="caption">(0 reviews)</Typography>
                        </Stack >
                    </div>

                    <Divider variant="middle"/>

                    {/* name and price */}
                    <Stack spacing={2} alignItems="center">
                        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>TULIP SKIRT BLACK</Typography>
                        <Typography variant="h6">4.349.000₫</Typography>
                    </Stack >

                    {/* pick color */}
                    <div className="">
                        <Typography variant="body1" sx={{ fontWeight: 'bold' }}>Color:</Typography>
                        <Stack direction="row" spacing={2} useFlexGap
                        sx={{ flexWrap: 'wrap' }}>
                            <label>
                                <input type='radio'/>
                                <span>Red</span>
                            </label>
                            
                        </Stack>
                    </div>

                    {/* pick size */}
                    <div className="">
                        <Typography variant="body1" sx={{ fontWeight: 'bold' }}>size:</Typography>
                        <Stack direction="row" spacing={2} useFlexGap
                        sx={{ flexWrap: 'wrap' }}>
                            <label>
                                <input type='radio'/>
                                <span>M</span>
                            </label>
                        </Stack>
                    </div>

                    {/* pick quantity */}
                    <div className="flex justify-center">
                        <div className="border w-3/4 rounded-full px-2 py-1 inline-block">
                            <Stack direction="row" spacing={4}
                            sx={{
                                width: '100%',
                                justifyContent: "center",
                                alignItems: "center",
                            }}>
                                <IconButton aria-label="minus">
                                    <RemoveIcon />
                                </IconButton>
                                <input type="number" value={0}
                                className="w-20 text-center outline-none"/>
                                <IconButton aria-label="add">
                                    <AddIcon />
                                </IconButton>
                            </Stack>
                        </div>
                    </div>

                    {/* nuttons */}
                    <Stack direction="row" spacing={4}>
                        <Button sx={{width:'50%', height: '6vh', border: '1px solid #FF9100', color: '#FF9100',fontWeight: 'bolder'}}>
                            Add To Cart
                        </Button>
                        <Button endIcon={<ArrowOutwardIcon />}
                        sx={{width:'50%', height: '6vh',background: '#DF301C', color: '#fff', fontWeight: 'bolder'}}
                        >
                            Buy Now
                        </Button>
                    </Stack>            
                </div>
            </div>

            {/* more details */}
            <div className="p-4">
                <DetailTab/>
            </div>

            <Divider variant="middle"/>

            {/* product similar */}
            <div className="p-4">
                <p>recommendation</p>
            </div>
        </div>
        <Footer/>
        </>
    )
}

export default DetailPage;