
import {Link} from "react-router-dom";
import {StoreList} from "@/features/stores/components/StoreList";

const StoreListScene: React.FC = () => {

  

    return (<div>
      <Link to="/">Home</Link>
      <Link to="/stores/new">New</Link>
      <h3>Stores</h3>
      <StoreList />
    </div>
    );
}

export {StoreListScene};