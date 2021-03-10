namespace QSLib

open System
module QuickSort =
    let partition (arr:array<int>, small: int, big: int): int =
        let pivot = arr.[big]
        let mutable i = small - 1
        let mutable med = 0
        for j in small .. (big - 1) do
            if arr.[j] < pivot then 
                i <- i + 1
                med <- arr.[i]
                arr.[i] <- arr.[j]
                arr.[j] <- med
        med <- arr.[i+1]
        arr.[i+1] <- arr.[big]
        arr.[big] <- med
        (i+1)


    let rec QSort (arr: array<int>, small: int, big: int) : array<int> =
        if small >= big then
            arr
        else
            let PivotInd = partition(arr, small, big)
            QSort(arr, small, (PivotInd - 1))
            QSort(arr, (PivotInd + 1), big)
            arr