import { ReactNode, useEffect, useState } from "react"
interface Props {
    id: string
}
const SingleAccount = ({ id }: Props) => {
    const [account, setAccount] = useState([])
    const fetchAccountData = () => {
        fetch("https://localhost:7115/account/" + id)
            .then(response => {
                return response.json()
            })
            .then(data => {
                setAccount(data)
            })
    }

    useEffect(() => {
        fetchAccountData()
    }, [])

    return (
        <h2>
            {account.fullname}
        </h2>
    );
}

export default SingleAccount