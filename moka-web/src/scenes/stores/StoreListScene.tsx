
import {Link, useLocation} from "react-router-dom";
import {StoreList} from "@/features/stores/components/StoreList";

const StoreListScene: React.FC = () => {
    const location = useLocation();
    const message = location.state?.message;

    return (<div>
      <Link to="/">Home</Link>
      <Link to="/stores/new">New</Link>
      <h3>Stores</h3>
      <div>{message && (<p>{message}</p>)}</div>
      <StoreList />
    </div>
    );
}

export {StoreListScene};