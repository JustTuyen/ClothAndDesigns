import NavBar from "../components/NavBar.jsx";
import Footer from "../components/Footer.jsx";
//

//

//
function Home(){
    return(
        <>
        <NavBar/>
        <div className="w-full min-h-screen bg-amber-300">
            <section className="bg-amber-50 w-3/4 mx-auto p-4">
                <h1>Home</h1>
            </section>
        </div>
        <Footer/>
        </>
    )
}

export default Home;