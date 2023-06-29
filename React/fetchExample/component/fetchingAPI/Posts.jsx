import { useState, useEffect, useMemo } from 'react'
import { LoadingSpinner } from '../loadingSpinner/LoadingSpinner'
import { Pagination } from '../pagination/Pagination'
import SingleAccount from './SingleAccount';

let itemPerPage = 1;

export const Posts = () => {
    const [errorMessage, setErrorMessage] = useState("")
    const [isLoading, setIsLoading] = useState(false)
    const [posts, setPosts] = useState([])
    const [currentPage, setCurrentPage] = useState(NaN)

    const controller = '/Post'
    const action = '/get-post-list'
    const fetchData = async () => {
        setIsLoading(true)
        const response = await fetch('https://localhost:7115/api' + controller + action)
        const data = await response.json().catch(e => {
            console.log(e)
            setErrorMessage("Unable to fetch data")
            setIsLoading(false)
        });
        setPosts(data)
        setCurrentPage(1);
        setIsLoading(false)
    }

    useEffect(() => {
        fetchData();
    }, []);


    const lastPage = Math.ceil(posts.length / itemPerPage);
    const currTableData = useMemo(() => {
        const firstPageIndex = (currentPage * itemPerPage) - 1;
        const lastPageIndex = firstPageIndex + itemPerPage;
        return posts.slice(firstPageIndex, lastPageIndex)
    }, [currentPage])

    const renderPost = (
        <div className='posts-container'>
            {currTableData.map((post) => (
                <div className="product-card" key={post.postId}>
                    <div className="product-img">
                        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoJBsIjRMDJdBK6QmlDiF1peqJGOwR8E_1Aw&usqp=CAU" alt="holder"></img>
                    </div>
                    <span>
                        <h2>
                            {post.productName}
                        </h2>
                        <h2>
                            {post.price}
                        </h2>
                        <SingleAccount id={post.accountId}></SingleAccount>
                    </span>
                </div>
            ))}
        </div>
    )
    return (
        <div className='Posts'>
            {isLoading ? <LoadingSpinner /> : renderPost}
            {errorMessage && <div className='error'>{errorMessage}</div>}

            {posts.length > 0 &&
                <>
                    <div className="spacing"></div>
                    <Pagination currentPage={currentPage} lastPage={lastPage} maxLength={7} setCurrentPage={setCurrentPage}></Pagination>
                </>
            }
        </div>
    )
}
