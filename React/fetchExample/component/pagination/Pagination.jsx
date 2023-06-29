import { getPaginationItems } from './lib/pagination.js';
import PageLink from "./PageLink";

import React from 'react'

export const Pagination = ({ currentPage, lastPage, maxLength, setCurrentPage }) => {
  const pageNums = getPaginationItems(currentPage, lastPage, maxLength);

    return (
        <nav className='pagination' aria-label='Pagination'>
            <PageLink
                href='#'
                disabled={currentPage === 1}
                onClick={() => setCurrentPage(currentPage - 1)}>Previous</PageLink>
            {pageNums.map((pageNum, index) => (
                <PageLink key={index} href='#' active={pageNum === currentPage} onClick={() => setCurrentPage(pageNum)}>
                    {!isNaN(pageNum) ? pageNum : '...'}
                </PageLink>
            ))}
            <PageLink
                href='#'
                disabled={currentPage === lastPage}
                onClick={() => setCurrentPage(currentPage + 1)}>Next</PageLink>
        </nav>
    )
}