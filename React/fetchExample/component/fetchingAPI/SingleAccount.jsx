import { useEffect, useState } from "react"
export const SingleAccount = ({ id }) => {
    const [account, setAccount] = useState([])
    const controller = 'account/api/Account'
    const action = '/get-account-by-id?id=' + id
    const fetchData = async () => {
        const response = await fetch('https://localhost:7115/' + controller + action)
        const data = await response.json().catch(e => {
            console.log(e)
        });
        setAccount(data)
    }

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <h2>
            {account.fullname}
        </h2>
    );
}

export default SingleAccount