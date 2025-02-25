import loading from "../assets/loading.svg";
import "./Loading.css";

const Loading = () => (
  <div className="spinner">
    <img src={loading} alt="Loading" />
  </div>
);

export default Loading;
