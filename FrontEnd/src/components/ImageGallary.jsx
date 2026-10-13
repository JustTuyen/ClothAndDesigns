
//
import SlickModule from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
const Slider = SlickModule.default ?? SlickModule;
//
import men from '../assets/men.webp';
import women from '../assets/women.webp';
//
import './css/ImgGallery.css'
//
function ImageGallery() {
  const images = [men, women, men, women];

  const settings = {
    customPaging: function (i) {
      return (
        <a>
          <img src={images[i]} alt={`Thumbnail ${i + 1}`} />
        </a>
      );
    },
    dots: true,
    dotsClass: "slick-dots slick-thumb",
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 2000,
    pauseOnHover: true,
  };

  return (
    <div className="slider-container">
      <Slider {...settings}>
        {images.map((image, index) => (
          <div key={index}>
            <img
              className="gallery-main-image"
              src={image}
              alt={`Gallery image ${index + 1}`}
            />
          </div>
        ))}
      </Slider>
    </div>
  );
}

export default ImageGallery;