
import {Link, useLocation} from "react-router-dom";
import {MachineList} from "@/features/machines/components/MachineList";

const MachineListScene: React.FC = () => {
    const location = useLocation();
    const message = location.state?.message;

    return (<div>
      <Link to="/">Home</Link>
      <p >New</p>
      <h3>Machines</h3>
      <div>{message && (<p>{message}</p>)}</div>
      <MachineList />
    </div>
    );
}

export {MachineListScene};