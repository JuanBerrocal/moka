import {Link} from "react-router-dom";

const HomeScene: React.FC = () => {
    return (
    <main>
        <header>
            <h1>Moka</h1>
            <p>Horeca machinery warehouse manager</p>
        </header>
        <div>
            <Link to="/stores">Stores</Link>
        </div>

    </main>);
}

export {HomeScene};