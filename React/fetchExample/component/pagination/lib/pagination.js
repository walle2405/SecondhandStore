export function getPaginationItems(
    currentPage,
    lastPage,
    maxLength,
) {
    const res = [];

    if (lastPage <= maxLength) {
        for (let i = 1; i <= lastPage; i++) {
            res.push(i);
        }
    }

    else {
        const firstPage = 1;
        const confirmedPagesCount = 3;
        const deductedMaxLength = maxLength - confirmedPagesCount; // 7 - 3 = 4
        const sideLength = deductedMaxLength / 2; // 4/2 = 2
        //Detect if number of page from edge to curr is greater than sideLength
        if (
            currentPage - firstPage < sideLength ||
            lastPage - currentPage < sideLength
        ) {
            for (let j = 1; j <= sideLength + firstPage; j++)
                res.push(j);
            res.push(NaN);
            for (let j = lastPage - sideLength; j <= lastPage; j++)
                res.push(j);
        }
        else if (
            currentPage - firstPage >= deductedMaxLength &&
            lastPage - currentPage >= deductedMaxLength
        ) {
            const deductedSideLength = sideLength - 1;

            res.push(1); res.push(NaN);

            for (let j = currentPage - deductedSideLength; j <= currentPage + deductedSideLength; j++) {
                res.push(j);
            }

            res.push(NaN); res.push(lastPage);
        }
        else {
            const isNearFirstPage = currentPage - firstPage < lastPage - currentPage;
            let remainingLength = maxLength;

            if (isNearFirstPage) {
                for (let j = 1; j <= currentPage + 1; j++) {
                    res.push(j);
                    remainingLength -= 1;
                }
                res.push(NaN);
                remainingLength -= 1;
                for (let j = lastPage - (remainingLength - 1); j <= lastPage; j++){
                    res.push(j);
                }
            }
            else {
                for (let j = lastPage; j >= currentPage - 1; j--) {
                    res.unshift(j);
                    remainingLength -= 1;
                }
                res.unshift(NaN);
                remainingLength -= 1;

                for (let j = remainingLength; j >= 1; j--) {
                    res.unshift(j);
                }
            }

        }
    }
    return res;
}
