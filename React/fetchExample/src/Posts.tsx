import { useEffect, useState } from "react"
import SingleAccount from "./SingleAccount"
const Posts = () => {
    const [posts, setPosts] = useState([])

    const fetchPostData = () => {
        fetch("https://localhost:7115/posts")
            .then(response => {
                return response.json()
            })
            .then(data => {
                setPosts(data)
            })
    }

    useEffect(() => {
        fetchPostData()
    }, [])

    return (
        <div>
            {posts.length > 0 && (
                <ul>
                    {posts.map(post => (
                        <li key={post.postId}>
                            <h2>
                                {post.productName}
                            </h2>
                            <span>
                                <h2>
                                    {post.price}
                                </h2>
                                <SingleAccount id={post.accountId}></SingleAccount>
                            </span>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}

export default Posts