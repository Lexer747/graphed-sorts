Imports System
Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Public Class Form1

    'initalization of global variables
    Inherits Form
    Dim CallCount, Trackbar, LargestFileValue, minA, maxA As Integer

    'these are used as tracking variables accross all sorts
    Dim SizeA, ArrayAccesses, ComparesMade, StackCounter, OverflowsOccured As Double

    'used as the variable to determine if the graph of the array should be updated live Or Not
    Dim animateGraph As Boolean

    'due to threading of the dropdowns it Is easier to store their current selected item globaly
    Dim ComboBoxItem As String

    'Hardcoded file path cuz im lazy
    Dim RealativePath As String = "C:\Users\Lexer\Documents\Visual Studio 2015\Projects\QuickSort Graphed\"

    'All these subs are used as templates for threading purposes
    Delegate Sub ThreadString(ByVal str As String)
    Delegate Sub ThreadInt(ByVal int As Integer)
    Delegate Sub ThreadPoint(ByVal str As String, ByVal int As Integer, ByVal int1 As Integer)
    Delegate Sub ThreadBind(ByVal str As String, ByVal int() As Integer)
    Delegate Sub Thread()

    Private Sub Sort()

        'intializes the counting variables
        ArrayAccesses = 0
        ComparesMade = 0
        OverflowsOccured = 0
        StackCounter = 0
        ComboBoxThreading(2)

        'select case used to find the length of the array to be sorted
        Select Case ComboBoxItem
            Case "File"
                SizeA = LengthOfFile("File.txt") - 1
            Case Else
                If Integer.TryParse(TextBox1.Text, SizeA) Then
                    SizeA = CInt(TextBox1.Text) - 1
                    If SizeA > 10000000 Then
                        SizeA = 10000000
                    End If
                Else
                    SizeA = 99
                End If
        End Select
        Dim Array(SizeA) As Integer

        'sets up the coordinate axis of the graph so that the data Is all included
        'also gets the range of values from the text box
        GetRange()
        ChartThreading(1)
        ChartThreading(2)
        ChartThreading(3)
        ChartThreading(4)

        'debug output
        Label1Threading("Debugging output" & vbNewLine & "Creating an array of " & SizeA + 1 & " numbers...")

        'select case used to find which type of array the user wishes to create
        ComboBoxThreading(2)

        Dim rand As New Random
        Select Case ComboBoxItem
            Case "Random"
                For i = 0 To SizeA
                    'creates random values between the ranges specified by the min And max values
                    Array(i) = rand.Next(minA, maxA + 1)
                Next
            Case "Random With order"

                'creates runs of data which are 5 element in a row in order
                'followed by another run of 5 etc
                RandomOrder(Array, SizeA)
            Case "Nearly Sorted"

                'creats data which is roughly sorted but only slightly off
                NearlySorted(Array, SizeA)
            Case "Reverse"

                'simply puts the array in reverse order
                'ingnores max and min currently
                'TODO scale the values
                For i = 0 To SizeA
                    Array(i) = SizeA - i
                Next
            Case "File"

                'gets an array from a file specified by file.txt
                ReadTextFile(Array, "File.txt")
            Case Else
                For i = 0 To SizeA
                    Array(i) = rand.Next(minA, maxA + 1)
                Next
        End Select

        'attach the array to the graph
        BindArray(" Array", Array)
        ChartThreading(5)

        'debug output
        Label1Threading(Label1.Text & vbNewLine & (" Created"))

        'save the array to a file for debugging
        savearray(Array, SizeA, "Unsorted.txt")
        Label1Threading(Label1.Text & vbNewLine & (" Saved"))

        'add the swapping series to the graph which Is in red
        ChartThreading(7)

        'display the big O notation for the sort
        ComboBoxThreading(1)
        DisplayPerformance(ComboBoxItem)

        'check if the graph needs to be updated live Or Not
        animateGraph = CheckBox1.CheckState

        'start a stopwatch as late as possible to accurately time the sort
        Dim stopwatch As New Stopwatch
        stopwatch.Start()

        'choose the sort based on the drop down And call it
        Select Case ComboBoxItem
            Case "INTRO"
                IntroSort(Array, SizeA)
            Case "QUICK"
                Quicksort(Array, 0, SizeA)
            Case "BUBBLE"
                BubbleSort(Array, SizeA)
            Case "COCKTAIL"
                CockTailSort(Array, SizeA)
            Case "INSERTION"
                InsertionSort(Array, SizeA)
            Case "BINARY INSERTION"
                BinaryInsertionSort(Array, 0, SizeA)
            Case "SHELL"
                ShellSort(Array)
            Case "COMB"
                CombSort(Array, SizeA)
            Case "MERGE"
                DoMergeSort(Array, 0, SizeA)
            Case "HEAP"
                HeapSort(Array, 0, SizeA + 1)
            Case "RADIX"
                RadixSort(Array, SizeA)
            Case "COUNT"
                CountSort(Array, SizeA)
            Case "BUCKET"
                BucketSort(Array, SizeA)
            Case "SELECTION"
                SelectionSort(Array, SizeA)
            Case "GNOME"
                GnomeSort(Array, SizeA)
            Case "SLOW"
                SlowSort(Array, 0, SizeA)
            Case "BOZO"
                BozoSort(Array, SizeA)
            Case "BOGO"
                BogoSort(Array, SizeA)
            Case Else
                IntroSort(Array, SizeA)
        End Select

        'stop the stopwatch as soon as it Is done to be more accurate
        stopwatch.Stop()

        'check if the sort was succesful as some sorts are Not always implemented properly
        'also debugging for New sorts
        If IsSorted(Array) = False Then
            MsgBox("failed To Sort")
            animateGraph = False
        End If

        'debug output
        Label1Threading(Label1.Text & vbNewLine & (" Sorted"))

        'display the time take to user
        Dim TimeTaken As Integer = stopwatch.ElapsedMilliseconds
        Label6Threading(FormatTime(TimeTaken))

        'remove the swapping series from the graph
        ChartThreading(1)
        ChartThreading(4)
        ChartThreading(6)
        chartpointorigin()
        BindArray(" Array", Array)
        ChartThreading(5)

        'update the counters
        Label15Threading(ComparesMade)
        Label16Threading(ArrayAccesses)
        Label15ThreadingR()
        Label16ThreadingR()

        'save the final array to a file for debugging
        savearray(Array, SizeA, "Sorted.txt")
        Label1Threading(Label1.Text & vbNewLine & (" Saved"))

        'compare the unsorted array to the final array to check if all the values are the same
        'And no values were lost Or changed
        If ValidateSort("Unsorted.txt", "Sorted.txt") = False Then
            MsgBox("Unstable sort occured")
        End If
    End Sub

    '---Initial Array Types---'
    Sub RandomOrder(ByRef Array() As Integer, ByVal Size As Integer)

        'fills an array passed to it with runs of numbers either positive runs Or negative
        Dim rand As New Random
        Dim Seed As Integer

        'this variable will detemine how many full runs can fit into the array
        'each run Is size 6
        Dim finish As Integer = (Size - (Size Mod 6 + 1))

        For i = 0 To finish Step 6
            'create a seed value between the min And max
            Seed = rand.Next(minA, maxA + 1)

            'if the seed will have a run, positive Or negative, which will go over max
            'Or go below min then just make the run of 6 the same value (seed)
            If Seed + 6 >= maxA And Seed - 6 >= minA Then
                For j = 0 To 5
                    Array(i + j) = Seed
                Next

                'if the seed will have a positive run which will go over the max, make the run negative
            ElseIf Seed + 6 >= maxA Then
                For j = 0 To 5
                    Array(i + j) = Seed - j
                Next

                'if the seed will have a negative run which will go under the min, make the run positive
            ElseIf Seed - 6 >= minA Then
                For j = 0 To 5
                    Array(i + j) = Seed + j
                Next
            Else

                'if the seed will Not overflow Or underflow, decide run based on odd Or even
                If Seed Mod 2 = 0 Then
                    For j = 0 To 5
                        Array(i + j) = Seed - j
                    Next
                Else
                    For j = 0 To 5
                        Array(i + j) = Seed + j
                    Next
                End If
            End If
        Next

        'for the final section of the array which Is Not size 6 create its own run of leftover length
        Seed = rand.Next(minA, maxA + 1)
        finish += 1

        'if the seed will have a run, positive Or negative, which will go over max
        'Or go below min then just make the run of 6 the same value (seed)
        If Seed + 6 >= maxA And Seed - 6 >= minA Then
            For j = 0 To Size Mod 6
                Array(finish + j) = Seed
            Next

            'if the seed will have a positive run which will go over the max, make the run negative
        ElseIf Seed + 6 >= maxA Then
            For j = 0 To Size Mod 6
                Array(finish + j) = Seed - j
            Next

            'if the seed will have a negative run which will go under the min, make the run positive
        ElseIf Seed - 6 >= minA Then
            For j = 0 To Size Mod 6
                Array(finish + j) = Seed + j
            Next
        Else

            'if the seed will Not overflow Or underflow, decide run based on odd Or even
            If Seed Mod 2 = 0 Then
                For j = 0 To Size Mod 6
                    Array(finish + j) = Seed - j
                Next
            Else
                For j = 0 To Size Mod 6
                    Array(finish + j) = Seed + j
                Next
            End If
        End If
    End Sub
    Sub NearlySorted(ByRef Array() As Integer, ByVal Size As Integer)

        'fills the array with data going from min to max with a random variance from the perfect fit
        Dim rand As New Random
        Dim Seed As Integer
        Dim Base As Integer
        Dim Scale As Double = (maxA - minA) / Size
        For i = 0 To Size
            Seed = Math.Pow(rand.Next(minA, maxA + 1), (1 / 2.1))
            Base = minA + (i * Scale)
            If Base + Seed > maxA Then
                Array(i) = Base - Seed
            ElseIf Base - Seed < minA Then
                Array(i) = Base + Seed
            Else
                If Base Mod 2 = 0 Then
                    Array(i) = Base - Seed
                Else
                    Array(i) = Base + Seed
                End If
            End If
        Next
    End Sub

    '---Hybrid Sorts---'
    Sub IntroSort(ByRef A() As Integer, Optional ByVal Size As Integer = -1, Optional ByVal Start As Integer = -1, Optional ByVal Finish As Integer = -1)
        'Best Case O(n * log(n))
        'Average Case O(n * log(n))
        'Worst Case O(n * log(n))

        'probably the most optimal sort i have used in this program

        'if the call for the sort does Not want to sort the whole array the else will sort between two specified Index's
        If Finish = -1 And Start = -1 Then
            IntroSortLoop(A, 0, Size, 2 * Math.Floor(Math.Log(Size)))
        Else
            Size = Finish - Start
            IntroSortLoop(A, Start, Finish, 2 * Math.Floor(Math.Log(Size)))
        End If
    End Sub
    Sub IntroSortLoop(ByRef A() As Integer, ByVal lo As Integer, ByVal hi As Integer, ByVal DepthLimit As Integer)

        'if the size of the array to sort if below 16 then just use insertion sort to Sort the values between the index's
        Track(1)
        While hi - lo > 16
            Track(2)

            'if it has been called to many times then a worst case may have been found so Switch to heap sort
            If DepthLimit = 0 Then
                Dim x As Integer = (hi - lo) + 1
                Dim copy(x - 1) As Integer
                Track(, 2 * x)

                'copies the section of the array which needs to be heapsorted to a temporary array
                Array.Copy(A, lo, copy, 0, x)

                'sorts the array
                HeapSort(copy, 0, x)

                'saves the sorted array back into the oringal array
                Array.Copy(copy, 0, A, lo, x)
                Return
            End If
            DepthLimit -= 1

            'partion the array based on a median of 3 pivot
            Dim p As Integer = Partition_B(A, lo, hi, MedianOf3_B(A, lo, (lo + ((hi - lo) / 2) + 1), hi - 1))

            'recursively call the sort again to further partion And sort the array
            IntroSortLoop(A, p, hi, DepthLimit)
            hi = p
        End While

        'this if Is to check if graph needs to be updated live if it does it makes a different call due to how the graph updates but it
        'Is less efficient so it Is Not done if it doesnt have to
        If animateGraph Then
            InsertionSort(A, SizeA, lo)
        Else
            InsertionSort(A, hi, lo)
        End If
    End Sub
    Function Partition_B(ByRef A() As Integer, ByVal lo As Integer, ByVal hi As Integer, ByVal x As Integer)

        'x Is the pivot value
        Dim i As Integer = lo
        Dim j As Integer = hi
        While True
            Track(4)
            While A(i) < x

                'keeping incrementing until you find a value less than the pivot
                Track(, 1)
                i += 1
            End While
            j -= 1
            While x < A(j)

                'keep decrementing until you find a value greater than the pivot
                Track(, 1)
                j -= 1
            End While

            'if array has been partitioned then return where the partition ends
            If Not (i < j) Then
                Return i
            End If

            'swap the values in the array so that the larger values move above the Partition
            Swap(A, i, j)
            i += 1
        End While

        'Shouldnt even happen but stops the compiler crying
        Return -1
    End Function
    Function MedianOf3_B(ByRef A() As Integer, ByVal lo As Integer, ByVal mid As Integer, ByVal hi As Integer)

        'takes 3 index's And finds the median value of the array based on the index's
        Track(1, 2)
        If A(mid) > A(lo) Then
            Track(1, 2)
            If A(hi) > A(mid) Then
                Track(, 1)
                Return A(mid)
            Else
                Track(1, 2)
                If A(hi) > A(lo) Then
                    Track(, 1)
                    Return A(hi)
                Else
                    Track(, 1)
                    Return A(lo)
                End If
            End If
        Else
            Track(1, 2)
            If A(hi) > A(mid) Then
                Track(1, 2)
                If A(hi) > A(lo) Then
                    Track(, 1)
                    Return A(lo)
                End If
            Else
                Track(, 1)
                Return A(hi)
            End If
            Track(, 1)
            Return A(mid)
        End If
    End Function

    '---Good Sorts---'
    Sub Quicksort(ByRef A() As Integer, ByVal low As Integer, ByVal high As Integer)
        'Worst Case O(n^2)
        'Best Case O(nlog(n))
        'Average Case O(nlog(n))

        'optimized quicksort but suffers from stackoverflows due to how many calls it makes
        'animation calls make up a large amount of calls and the recursion is extensive

        'if the partition size is 16 or less then use insertion sort
        Track(1)
        If (high - low + 1) <= 16 Then

            'binaryinsertionsort not used as that uses a recursive binary search which uses too much of the stack for the sort to work on larger arrays
            InsertionSort(A, high, low)
        Else

            'normal partition using median of 3 
            Dim Median As Integer = MedianOf3_A(A, low, (high + low) / 2, high)
            Dim Partition As Integer = Partition_A(A, low, high, Median)
            Quicksort(A, low, Partition - 1)
            Quicksort(A, Partition, high)
        End If
    End Sub
    Function MedianOf3_A(ByRef A() As Integer, ByVal low As Integer, ByVal mid As Integer, ByVal high As Integer)

        'takes 3 index's and finds the median value to use as a pivot
        'puts the pivot in the correct place (low index)
        Track(1, 3)
        If A(mid) > A(low) Then
            Swap(A, low, mid)
        Else
            Track(1, 2)
            If A(high) > A(mid) Then
                Swap(A, low, high)
            Else
                Swap(A, low, mid)
            End If
        End If
        Return A(low)
    End Function
    Function Partition_A(ByRef A() As Integer, ByVal low As Integer, ByVal high As Integer, ByVal pivot As Integer)

        'based on the Hoare parition alogrithim
        'partitions the data based on a pivot value moving all data larger to other side of it And all smaller data below
        Track(1)
        While low < high
            Track(6, 2)
            While low < high And A(low) <= pivot
                Track(2, 1)
                low += 1
            End While
            While high >= low And A(high) > pivot
                Track(2, 1)
                high -= 1
            End While
            If low < high Then
                Swap(A, low, high)
            End If
        End While
        Return high + 1
    End Function

    Sub DoMergeSort(ByVal A() As Integer, ByVal low As Integer, ByVal high As Integer)
        'Worst Case O(nlog(n))
        'Best Case O(nlog(n))
        'Average Case O(nlog(n))

        Track(1)
        If low >= high Then Return
        Dim length As Integer = high - low + 1
        Dim middle As Integer = Math.Floor((low + high) / 2)

        'recurseivly calls merge sort on the lower half of the array until it reaches the end
        DoMergeSort(A, low, middle)

        'calls merge sort on the upper half of the array until it reaches the end
        DoMergeSort(A, middle + 1, high)

        'this breaks down the array to the smallest element, then it compares the pairs of element ordering
        'it repeats this process even on groups of elements appearing to 'merge' them together

        'creates a temporary array to store the merges
        Dim temp(A.Length - 1) As Integer
        For i As Integer = 0 To length - 1
            Track(1, 1)
            temp(i) = A(low + i)
        Next
        Dim m1 As Integer = 0
        Dim m2 As Integer = middle - low + 1

        'the comparision of each of the elements is performed here
        For i As Integer = 0 To length - 1
            Track(2)
            If m2 <= high - low Then
                Track(1)
                If m1 <= middle - low Then
                    Track(1)
                    If temp(m1) > temp(m2) Then
                        A(i + low) = temp(m2)
                        m2 += 1
                        Track(, 1)
                    Else
                        A(i + low) = temp(m1)
                        m1 += 1
                        Track(, 1)
                    End If
                Else
                    A(i + low) = temp(m2)
                    m2 += 1
                    Track(, 1)
                End If
            Else
                A(i + low) = temp(m1)
                m1 += 1
                Track(, 1)
            End If
            If animateGraph Then
                Animate(A, i + low, SizeA)
            End If
        Next
    End Sub

    Sub CountSort(ByRef A() As Integer, ByVal size As Integer)
        'Worst Case O(n + r)
        'Best Case O(n + r)    
        'Average Case O(n + r)
        'r = range of values in array

        'find the max and min of the array in linear time
        Dim Min As Integer = maxA
        Dim Max As Integer = minA
        Track(3)
        For i = 0 To size
            Track(3, 2)
            If A(i) > Max Then
                Track(, 1)
                Max = A(i)
            End If
            If A(i) < Min Then
                Track(, 1)
                Min = A(i)
            End If
        Next

        'create temp space the size of the range of values in the array
        Dim Count(Max - Min + 1) As Integer
        For i = 0 To size

            'for each unique integer add one to its count in the array
            Track(1, 1)
            Count(A(i) - Min) += 1
        Next
        Dim j As Integer

        'for all the values in the range
        For i = Min To Max
            Track(1)
            While Count(i - Min) > 0
                'for the number of appearences for each unique integer insert it back into the array
                Track(1, 1)
                A(j) = i
                If animateGraph Then
                    Animate(A, j, SizeA)
                End If
                j += 1

                'take one away from the number of times that value appears
                Count(i - Min) -= 1

                'stop the loop for that value when it reaches 0
            End While
        Next
    End Sub

    Sub HeapSort(ByRef A() As Integer, ByVal start As Integer, ByVal last As Integer)
        'Worst Case O(nlog(n))
        'Best Case O(nlog(n))
        'Average Case O(nlog(n))

        'running in linear time it creates a binary tree based on the array
        'the largest value is at index 0, the root
        For i = Math.Ceiling((last - 1) / 2) To start Step -1
            DoHeap(A, i, last - 1)
        Next

        'for the full tree starting with root
        For i = last - 1 To start Step -1

            'this swaps the root value with the last value of the tree, hence sorting the root
            'since it is sorted the length of the tree is one less, hence why the 'for Loop ' decrements
            Swap(A, i, start)

            'rebuild the binary tree as it will be broken after the value was swapped
            DoHeap(A, start, i - 1)
        Next
    End Sub
    Sub DoHeap(ByRef A() As Integer, ByVal start As Integer, ByVal last As Integer)

        'put elements at index start in the binary tree at the end and fix any elements up the tree

        'element to add to tree
        Track(1, 2)
        Dim temp As Integer = A(start)
        Dim k As Integer

        'while the tree is broken
        While start <= last / 2
            Track(3, 3)

            'pointer to child of a specified index
            'leftchild index = 2*i + 1
            'rightchild index = 2*i + 2
            k = 2 * start

            'while the children of a node are in the wrong order
            While k < last AndAlso A(k) < A(k + 1)
                Track(2, 2)
                k += 1
            End While

            'if the element you want to insert is larger than its children then exit
            If temp >= A(k) Then
                Exit While
            End If

            'else swap the pearent with the child to fix the heap
            A(start) = A(k)
            If animateGraph Then
                Animate(A, start, SizeA, k)
            End If

            'the new pearent is the index of the swapped value
            start = k
        End While

        'insert the node into the tree
        A(start) = temp
        If animateGraph Then
            Animate(A, start, SizeA)
        End If
    End Sub

    Sub RadixSort(ByRef A() As Integer, ByVal Size As Integer)
        'Best Case O(k * n)
        'Average Case O(k * n)
        'Worst Case O(k * n)
        'where k = number of digits of the largest value

        'finds the largest value in linear time
        Dim Max As Integer = 0
        Track(2)
        For i = 0 To Size
            Track(2, 2)
            If A(i) > Max Then
                Track(, 1)
                Max = A(i)
            End If
        Next

        'finds then number of digits from the max value and converts it to a power of 10
        Dim Finish As Integer = Math.Pow(10, Max.ToString.Length)
        Dim Place As Integer = 1

        'for each digit of the values in the array, sort them based on that digit
        Do Until Place >= Finish
            Track(2)
            CountingSort2(A, Place, Size)
            Place *= 10
        Loop
    End Sub
    Sub CountingSort2(ByRef A() As Integer, ByVal Place As Integer, ByVal Size As Integer)
        Dim out(Size) As Integer
        Dim count(10) As Integer
        Dim temp As Integer
        Track(13)
        For i = 0 To Size
            Track(1, 1)

            'find the value of the digit of the element you are sorting
            ArrayAccesses += 1
            temp = (Math.Floor(A(i) / Place)) Mod 10

            'add that to the counting array
            count(temp) += 1
        Next
        For i = 1 To 10

            'make it so the total times it has to count is the size
            count(i) += count(i - 1)
        Next
        For i = Size To 0 Step -1
            Track(1, 2)

            'populate the storage array with the elements based on the digits
            temp = (Math.Floor(A(i) / Place)) Mod 10
            out(count(temp) - 1) = A(i)
            count(temp) -= 1
        Next
        For i = 0 To Size
            Track(1, 1)

            'put the values back into the array based on the digit they were sorted
            A(i) = out(i)
            If animateGraph Then
                Animate(A, i, Size)
            End If
        Next
    End Sub

    '---Ok Sorts---'
    Sub BucketSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(n^2)
        'Best Case O(n + k)
        'Average Case O(n + k)
        Dim Max As Integer = 0
        Dim Min As Integer = A(0)
        Track(4)

        'find the max and min in linear time
        For i = 0 To Size
            Track(2, 2)
            If A(i) > Max Then
                Track(, 1)
                Max = A(i)
            End If
            If A(i) < Min Then
                Track(, 1)
                Min = A(i)
            End If
        Next

        'best number of buckets is the Square root of the size due to the time complexity of insertion sort (O(n^2))
        Dim NumOfBuckets As Integer = Math.Sqrt(Size)
        Dim Range As Integer = (Max - Min) / NumOfBuckets

        'create a dynamic jagged array (best name ever)
        Dim Buckets()() As Integer = New Integer(NumOfBuckets)() {}

        'intialize buckets with size 1 and the value -1
        For i = 0 To NumOfBuckets
            Track(1)
            Buckets(i) = New Integer(0) {}
            Buckets(i)(0) = -1
        Next

        'loop through the array groupping the elements based on which multiple of the Range they are
        'if they are in that range, then they are moved to the next avaliable index of the bucket array
        Dim temp, x As Integer
        For i = 0 To Size
            Track(1, 1)

            'which bucket they belong is found with this calculation
            temp = Math.Round(A(i) / Range, 0)
            x = 0
            Do

                'this if statement determines if the array needs to be increased in size
                Track(3)
                If x > Buckets(temp).GetUpperBound(0) Then

                    'if it does then it copys the array to temporary storage
                    Dim copy(x) As Integer
                    Array.Copy(Buckets(temp), copy, x)

                    'then it increases the size
                    Buckets(temp) = New Integer(x) {}

                    'and copies the old data back to the new sized array
                    Array.Copy(copy, Buckets(temp), x)
                End If
                If Buckets(temp)(x) = 0 Or Buckets(temp)(x) = -1 Then

                    'if the next slot in the bucket is empty fill it with value
                    Track(, 1)
                    Buckets(temp)(x) = A(i)
                    Exit Do
                Else

                    'if the next slot wasnt empty increase the index by one
                    x += 1
                End If
            Loop
        Next

        'this pointer keeps track overall buckets where they should be added back to the Array
        Dim pointer As Integer = 0
        For j = 0 To NumOfBuckets
            Track(1)
            If Buckets(j)(0) = -1 Then
                'empty bucket
            Else

                'bucket has values which need to be sorted
                Dim upper As Integer = Buckets(j).GetUpperBound(0)
                Dim copy(upper) As Integer

                'copies the bucket to tempary space
                Array.Copy(Buckets(j), copy, upper + 1)

                'sorts the temporary array
                BinaryInsertionSort(copy, 0, upper)

                'adds the array to the starting array based on the pointer for all Buckets
                Track(1)
                For i = 0 To upper
                    Track(1, 1)
                    A(pointer) = copy(i)
                    pointer += 1
                    If animateGraph Then
                        Animate(A, pointer, Size)
                    End If
                Next
            End If
        Next
    End Sub

    Sub SelectionSort(ByRef A() As Integer, ByVal size As Integer)
        'Worst Case O(n^2)
        'Best Case O(n^2)
        'Average Case O(n^2)

        'even though the big O looks bad (worse than bubble sort), as it is worse than many sorts it runs very well on modern machines due to caching of memory being
        'faster than comparisons. And this sort has relatively low amount of comparisons And always runs in the same amount of time which helps its speed

        'for each index of the array
        Track(1)
        For j = 0 To size - 1
            Dim Min As Integer = j

            'for the rest of index which havent been sorted
            Track(2)
            For i = j + 1 To size
                Track(2, 2)
                'finds the smallest value
                If A(i) < A(Min) Then
                    Min = i
                End If
            Next

            'if the smalled value isnt equal to the index you want to swap with then swap them
            If Min <> j Then
                Swap(A, j, Min)
            End If
        Next
    End Sub

    Sub InsertionSort(ByRef A() As Integer, ByVal size As Integer, Optional ByVal Min As Integer = 0)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O(n^2)

        'better version of selection sort, its speed is based on how far away each element Is From its finishing posistion so it runs poorly on revervse data
        Dim j As Integer = 0
        Dim k As Integer = 0

        'for all the values within the specfifec range
        Track(1)
        For i = Min + 1 To size
            Track(3, 3)
            k = A(i)
            j = i - 1

            'this inner loop swaps all values less than the k until they reach the correct Index
            While i > 0 And A(j) > k And j >= 0
                Track(1, 2)
                'swap the values which are the wrong order
                A(j + 1) = A(j)
                If animateGraph Then
                    Animate(A, j + 1, A.Length - 1, j)
                End If

                'make the pointer for the next value one less
                j = j - 1
                If j = -1 Then

                    'if the end of the array is reached, exit the loop as this value is the New smallest
                    Exit While
                End If
            End While

            'insert the value to sort into the correct position
            A(j + 1) = k
            If animateGraph Then
                Animate(A, j + 1, A.Length - 1)
            End If
        Next
    End Sub

    Sub BinaryInsertionSort(ByRef A() As Integer, ByVal Start As Integer, ByVal Finish As Integer)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O(n^2)

        'better version of insertion sort by finidng the index to insert the next value by using binary search on the bottom of the array 
        'which is garneteed to be sorted  hence less time is spent inserting the value

        'faster to hardcode arrays this small
        Track(3)
        If Finish - Start <= 0 Then
            Return
        ElseIf Finish - Start = 1 Then
            Track(1, 2)
            If A(Start) > A(Finish) Then
                Swap(A, Start, Finish)
                Return
            End If
        End If

        'only works With bottom up sorts When used In combination, hence Not used for intro sort
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim x As Integer = 0

        'for the size of the array
        For i = Start To Finish
            Track(3, 2)
            j = i - 1

            'k = current value to be inserted
            k = A(i)

            'x = index to insert value
            x = BinarySearch(A, Start, j, k)

            'this while loop inserts the value into its index swapping values as it goes
            While j >= x And j >= 0
                Track(2, 2)
                A(j + 1) = A(j)
                If animateGraph Then
                    Animate(A, j + 1, A.Length - 1, j)
                End If
                j -= 1
            End While

            'insert the value
            A(j + 1) = k
            If animateGraph Then
                Animate(A, j + 1, A.Length - 1)
            End If
        Next
    End Sub
    Function BinarySearch(ByVal A() As Integer, ByVal low As Integer, ByVal high As Integer, ByVal Search As Integer)

        'performs a binary search on an array between 2 specified index's
        'returns the index it is found +1 hence the next space the value should be inserted into
        Track(1)
        If high <= low Then
            Track(1, 1)
            If Search > A(low) Then
                Return low + 1
            Else
                Return low
            End If
        End If
        Dim mid As Integer = (low + high) / 2
        Track(1, 1)
        If Search = A(mid) Then
            Return mid + 1
        End If
        Track(1, 1)
        If Search > A(mid) Then
            Return BinarySearch(A, mid + 1, high, Search)
        Else
            Return BinarySearch(A, low, mid - 1, Search)
        End If
    End Function

    Sub CombSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O((n^2)/(2^1.3))

        'combs through the array swapping values which are the wrong way round which are A 'gap' apart
        Dim gap As Integer = Size
        Dim shrink As Decimal = 1.3
        Dim swapped As Boolean
        Dim i As Integer
        Track(2)

        'do until the gap size is one, as this is bubble sort
        'but it is time efficient as bubble sort is based on how far away elements are From thier final index
        'since they will only be one away it is very quick
        Do Until gap = 1 And swapped = False

            'reduce the gap size of the comb
            gap = Int(gap / shrink)
            Track(2)
            If gap < 1 Then

                'if the gap is less than one make it one
                gap = 1
            End If
            i = 0
            swapped = False

            'for the size of the array use this comb size to swap all elements in each comb run which are incorrect
            Do Until i + gap >= Size + 1
                Track(2, 2)
                If A(i) > A(i + gap) Then
                    Swap(A, i, i + gap)
                    swapped = True
                End If
                i += 1
            Loop
        Loop
    End Sub

    Sub ShellSort(ByRef A() As Integer)
        'Worst Case O(n^2)
        'Best Case O(n * log2(n))
        'Average Case O(n^2)

        'splitting the array into sub array where the index of the these sub arrays is i + increment until that equals the size
        'eg array of 0,1,2,3,4,5,6,8,9
        'one the first pass it is split into these sub arrays which are sorted by the most inner loop which performs insertion sort
        '0,5
        '1,6
        '2,7
        '3,8
        '4,9
        'in the second pass the increment is divided by 2.2 this is repeated until it  reaches 0 hence performing insertion sort over the entire array

        Dim j As Integer
        Dim temp As Integer
        Dim increment As Integer

        'starting sub array size is 2 big so the increment must be half the max
        increment = Int(SizeA / 2)

        'this is the loop that 'passes' over the array once insertion has been completed On the entire array it exits
        Track(1)
        While increment > 0

            'for one insertion sort over each sub array
            Track(1)
            For i = 0 To SizeA
                Track(3, 3)
                j = i
                temp = A(i)

                'this while is the insertion sort for the sub arrays
                While j >= increment And A(Math.Abs(j - increment)) > temp
                    Track(2)

                    'if the j value is large enough to not cause underflow compare it the Index of its smaller subarray swap values until the temp value Is insterted into the sub Array
                    A(j) = A(j - increment)
                    j = j - increment
                    If animateGraph Then
                        Animate(A, j, SizeA, i)
                    End If
                End While
                A(j) = temp
            Next

            'mathmatically this is the best decrement size for a shell sort
            increment = (increment / 2.2)
        End While
    End Sub

    '---Very Bad Sorts---'
    Sub CockTailSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O(n^2)

        'this sort is so slow its comical, it is variation of bubble sort but somehow runs slower on modern machines
        'probably due to caching of how the computer intrepets having two large loops which require reassagining the cache twice each pass

        Dim swapped As Boolean

        'go through the array swapping pairs of values which are the wrong way round
        'exit if no values had to be swapped as the array will be sorted
        Do
            swapped = False

            'step up through the array once swapping all values
            Track(3)
            For i = 0 To Size - 1
                Track(1, 2)

                'if the next value is bigger then swap them to put them in correct order
                If A(i) > A(i + 1) Then
                    Swap(A, i, i + 1)
                    swapped = True
                End If
            Next

            'if it is sorted exit the loop
            If swapped = False Then
                Exit Do
            End If
            swapped = False

            'step down through the array swapping all values not in order
            Track(1)
            For i = Size - 1 To 0 Step -1
                Track(1, 2)

                'if the next value is bigger then swap them to put them in correct order
                If A(i) > A(i + 1) Then
                    Swap(A, i, i + 1)
                    swapped = True
                End If
            Next
        Loop While swapped = True

    End Sub

    Sub BubbleSort(ByRef A() As Integer, ByVal n As Integer)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O(n^2)

        'a simple sort that should never be used in any serious program

        Dim Swapped As Boolean

        'do until no swaps have occured as then it is sorted
        Do
            Track(2)
            Swapped = False
            For i = 1 To n
                Track(2, 2)

                'if the previous value was smaller then swap the values as they brings them closer to being sorted
                If A(i - 1) > A(i) Then
                    Swap(A, i, i - 1)
                    Swapped = True
                End If
            Next
        Loop Until Swapped = False
    End Sub

    Function SlowSort(ByVal A() As Integer, ByVal i As Integer, ByVal j As Integer)
        'Worst Case O(n^log(n))
        'Best Case O(n^log(n))
        'Average Case O(n^log(n))

        'well this implmentation is horrednous

        'attempted to actually get a sort to finish with 100 elements
        'to do this use a gloabl varible to keep rough track how many calls have been made
        'once this reaches this abirtuary limit, simply exit the function and try to finish the rest of the called functions

        StackCounter += 1
        If StackCounter = 4500 Then
            Track(,, 1)
            StackCounter = 0
            Return 0
        End If

        'the surrender case, once the size of the recursive array reaches 1 return as this Array Is garenteed to be sorted
        Track(1)
        If i >= j Then
            Return 0
        End If

        'split the array in half
        Dim m = (i + j) / 2

        'call the lower half recursively
        SlowSort(A, i, m)
        If StackCounter = 4500 Then
            Track(,, 1)
            StackCounter = 0
            Return 0
        End If

        'call the upper half
        SlowSort(A, m + 1, j)
        If StackCounter = 4500 Then
            Track(,, 1)
            StackCounter = 0
            Return 0
        End If

        'swap the middles value of the passed array with the final index if they are the  wrong order
        'the actual sorting of the array occurs here
        'it only occurs once per call and it only swaps the middle with final if they are wrong hence why this thing is so slow
        Track(1, 2)
        If A(m) > A(j) Then
            Swap(A, m, j)
        End If

        'the bit that makes it super slow
        'call the rest of the array which has not been sorted yet
        SlowSort(A, i, j - 1)
        Return 0
    End Function

    Sub GnomeSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(n^2)
        'Best Case O(n)
        'Average Case O(n^2)

        'derpy insertion sort :)

        'while the pointer hasnt reached the final index
        Dim Pointer As Integer = 1
        Track(1)
        While Pointer <= Size
            Track(1, 2)

            'if the previous index and the current index of the pointer are inorder then  increase the pointer
            If A(Pointer) >= A(Pointer - 1) Then
                Pointer += 1
            Else

                'else swap the values and decrease the pointer as the rest of the array will be out of order
                Swap(A, Pointer, Pointer - 1)
                Track(1)

                'dont decrease the pointer if it is at the end of the array
                If Pointer > 1 Then
                    Pointer -= 1
                End If
            End If
        End While
    End Sub

    Sub BozoSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(infinite)
        'Best Case O(n)
        'Average Case O(n * n!)

        'dont need to explain this code
        Dim random As New Random
        Dim rand1, rand2 As Integer
        Do
            rand1 = random.Next(0, Size)
            rand2 = random.Next(0, Size)
            Swap(A, rand1, rand2)
            Track(1)
        Loop While IsSorted(A, Size) = False
    End Sub

    Sub BogoSort(ByRef A() As Integer, ByVal Size As Integer)
        'Worst Case O(infinite)
        'Best Case O(n) 
        'Average Case O(n * n!)

        'shuffles the entire array before checking if it is sorted
        Do
            ShuffleArray(A, Size)
            Track(1)
        Loop While IsSorted(A, Size) = False
    End Sub
    Sub ShuffleArray(ByRef A() As Integer, ByVal Size As Integer)

        'based on the Fisher-Yates shuffle

        'shuffles an array in linear time
        Dim rand As Integer
        Dim random As New Random

        'while there are still elements to shuffle
        Do While Size > 0

            'pick one of the remaing elements
            rand = random.Next(0, Size)

            'decrease the number of elements
            Size -= 1

            'swap the element
            Swap(A, Size, rand)
        Loop
    End Sub

    '---Checks and search---'
    Function IsSorted(ByVal A() As Integer, Optional ByVal size As Integer = -1)

        'linear search through an array returning true if the elements lie in ascending order
        If size = -1 Then
            size = SizeA
        End If
        Track(2)
        For i = 0 To size - 1
            Track(2, 2)
            If A(i) > A(i + 1) Then
                Return False
            End If
        Next
        Return True
    End Function

    Sub Swap(ByRef A() As Integer, ByVal Pos1 As Integer, ByVal Pos2 As Integer)

        'swaps two index's using temporary storage
        Dim temp As Integer = A(Pos1)
        A(Pos1) = A(Pos2)
        A(Pos2) = temp
        Track(, 4)

        'calls animation since a sort will have done something which can be displayed on the graph
        If animateGraph Then
            Animate(A, Pos1, A.Length - 1, Pos2)
        End If
    End Sub

    '-- -graph animation & UI-- -'
    Sub GetRange()

        'go to each textbox and uses integer validation provided by vb to check if they are valid
        'if they are valid the new max and min values for the rng are set
        If Integer.TryParse(TextBox2.Text, minA) Then
            minA = CInt(TextBox2.Text)
            If minA < 1 Then
                minA = 1
            ElseIf minA > 100000 Then
                minA = 100000
            End If
        Else
            minA = 1
        End If
        If Integer.TryParse(TextBox3.Text, maxA) Then
            maxA = CInt(TextBox3.Text)
            If maxA < 1 Then
                maxA = 1
            ElseIf maxA > 100000 Then
                maxA = 100000
            End If
        Else
            maxA = 1000
        End If

        'if the min is larger than the max, swap them around
        If minA > maxA Then
            Dim x As Integer = minA
            minA = maxA
            maxA = x
        End If
    End Sub

    Sub DisplayPerformance(ByVal ComboBox As String)

        'changes the text to show big O for each sort
        Select Case ComboBoxItem
            Case "TIM"
                RefreshPerformance("n * log(n)", "n * log(n)", "n * log(n)")
            Case "INTRO"
                RefreshPerformance("n * log(n)", "n * log(n)", "n * log(n)")
            Case "QUICK"
                RefreshPerformance("n^2", "n * log(n)", "n * log(n)")
            Case "BUBBLE"
                RefreshPerformance("n^2", "n", "n^2")
            Case "RADIX"
                RefreshPerformance("n * k", "n * k", "n * k")
            Case "BUCKET"
                RefreshPerformance("n^2", "n + k", "n + k")
            Case "COCKTAIL"
                RefreshPerformance("n^2", "n", "n^2")
            Case "INSERTION"
                RefreshPerformance("n^2", "n", "n^2")
            Case "BINARY INSERTION"
                RefreshPerformance("n^2", "n", "n^2")
            Case "SHELL"
                RefreshPerformance("n^2", "n * log(n)", "n^2")
            Case "COMB"
                RefreshPerformance("n^2", "n ", "n^2 / 2^1.3")
            Case "MERGE"
                RefreshPerformance("n * log(n)", "n * log(n)", "n * log(n)")
            Case "HEAP"
                RefreshPerformance("n * log(n)", "n * log(n)", "n * log(n)")
            Case "COUNT"
                RefreshPerformance("n + range", "n + range", "n + range")
            Case "SELECTION"
                RefreshPerformance("n^2", "n^2", "n^2")
            Case "GNOME"
                RefreshPerformance("n^2", "n", "n^2")
            Case "SLOW"
                RefreshPerformance("n ^ log(n)", "n ^ log(n)", "n ^ log(n)")
            Case "BOZO"
                RefreshPerformance("infinite", "n", "n * n!")
            Case "BOGO"
                RefreshPerformance("infinite", "n", "n * n!")
            Case Else
                RefreshPerformance("n * log(n)", "n * log(n)", "n * log(n)")
        End Select
    End Sub
    Sub RefreshPerformance(ByVal WorstCase As String, ByVal BestCase As String, ByVal AverageCase As String)

        'changes the text inside the labels to the big O passed to it
        Label20Threading("O(" & WorstCase & ")")
        Label24Threading("O(" & BestCase & ")")
        Label25Threading("O(" & AverageCase & ")")
    End Sub

    Sub Animate(ByVal A() As Integer, ByVal pos1 As Integer, ByVal Size As Integer, Optional ByVal pos2 As Integer = -1)

        'updates the global counter variables
        Label15Threading(ComparesMade)
        Label16Threading(ArrayAccesses)
        Label17Threading(OverflowsOccured)

        'refreshs the graph with the swapped values
        GraphUpdate(A, pos1, pos2, Size)

        'refresh the text for the global counter variables
        Label15ThreadingR()
        Label16ThreadingR()
        Label17ThreadingR()
    End Sub
    Sub GraphUpdate(ByVal A() As Integer, ByVal pos1 As Integer, ByVal pos2 As Integer, ByVal Size As Integer)

        'uses two temp arrays to store the values for the array on the graph, the second temp Is for the swapping index's
        Dim temp(Size) As Integer
        Dim temp2(Size) As Integer

        'if pos2 is -1 then index's arnt being swapped it is simply changing value and needs to be updated
        If pos2 = -1 Then
            For i = 0 To Size

                'if the index is the value which is being chnaged put it on the temp2 Array
                If i = pos1 Then
                    temp2(i) = A(i)
                    temp(i) = 0
                Else

                    'else put it on the other temp array
                    temp(i) = A(i)
                    temp2(i) = 0
                End If
            Next
        Else

            'if the value is being swapped
            For i = 0 To Size

                'if the index is a swapped value put it on the temp2 array
                If i = pos1 Or i = pos2 Then
                    temp2(i) = A(i)
                    temp(i) = 0
                Else
                    temp(i) = A(i)
                    temp2(i) = 0
                End If
            Next
        End If

        'attach the array to the graph
        BindArray(" Array", temp)
        BindArray("Swapping", temp2)

        'update the graph
        ChartThreading(5)

        'read the trackbar
        ReadTrackBar()
        If Trackbar <> 0 Then

            'delay the thread based on the trackbar value
            System.Threading.Thread.Sleep(Trackbar * 10)
        End If
    End Sub

    Sub Track(Optional ByVal C As Integer = 0, Optional ByVal A As Integer = 0, Optional ByVal O As Integer = 0)
        ComparesMade += C
        ArrayAccesses += A
        OverflowsOccured += O
    End Sub

    Sub InitializeDropDowns()

        'fills the drop downs with a list of sorts and start array types
        With ComboBox1.Items
            .Add("INTRO")
            .Add("QUICK")
            .Add("MERGE")
            .Add("HEAP")
            .Add("INSERTION")
            .Add("BINARY INSERTION")
            .Add("SHELL")
            .Add("COMB")
            .Add("RADIX")
            .Add("COUNT")
            .Add("BUCKET")
            .Add("SELECTION")
            .Add("BUBBLE")
            .Add("COCKTAIL")
            .Add("GNOME")
            .Add("SLOW")
            .Add("BOZO")
            .Add("BOGO")
        End With
        ComboBox1.Text = "INTRO"
        With ComboBox2.Items
            .Add("Random")
            .Add("Random With order")
            .Add("Nearly Sorted")
            .Add("Reverse")
            .Add("File")
        End With
        ComboBox2.Text = "Random"
    End Sub

    Sub InitializeLabels()

        'fills the starting labels with the base values
        TextBox1.Text = "100"
        TextBox2.Text = "1"
        TextBox3.Text = "1000"
        CheckBox1.CheckState = CheckState.Checked
    End Sub

    '---File stuff---'
    Sub savearray(ByVal A() As Integer, ByVal size As Integer, ByVal FileName As String, Optional ByVal Append As Boolean = False)

        'the path is hardcoded and probably should be changed to a dynamic path
        Dim Path As String = RealativePath & FileName

        'if the file doesnt exist at the path then create the file
        If My.Computer.FileSystem.FileExists(Path) = False Then
            Dim fs As System.IO.FileStream = File.Create(Path)
            fs.Close()
        End If

        'write the array with csv to the file
        Dim FileWrite As System.IO.StreamWriter
        FileWrite = My.Computer.FileSystem.OpenTextFileWriter(Path, Append)
        For i = 0 To size
            FileWrite.Write(A(i).ToString & ",")
        Next
        FileWrite.Close()
    End Sub

    Function LengthOfFile(ByVal FileName As String)

        'finds the length of the array stored in the file and the largest value it holds
        Dim i As Integer

        'the path is hardcoded and probably should be changed to a dynamic path
        Dim Path As String = RealativePath & FileName

        'if the file doesnt exist, create it
        If My.Computer.FileSystem.FileExists(path) = False Then
            Dim fs As System.IO.FileStream = File.Create(path)
            fs.Close()
        End If
        LargestFileValue = 0

        'using vbs built in file readers
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            'specify how the data is read and the fields are seprated
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim CurrentRow() As String

            'read till end of file
            While Not MyReader.EndOfData

                'for the next piece of data seperated by a comma try
                Try

                    'read all the fields into the array
                    CurrentRow = MyReader.ReadFields()
                    Dim CurrentFields As String

                    'for all the fields read into the array
                    For Each CurrentFields In CurrentRow

                        'if value of the field is larger overwrite it so that the largest value is found

                        If CInt(CurrentFields) > LargestFileValue Then
                            LargestFileValue = CInt(CurrentFields)
                        End If

                        'add one for each field
                        i += 1
                    Next
                Catch ex As Exception
                End Try
            End While
        End Using

        'return the length of the array
        Return i
    End Function

    Sub ReadTextFile(ByRef A() As Integer, ByVal FileName As String)

        'the path is hardcoded and probably should be changed to a dynamic path
        Dim Path As String = RealativePath & FileName

        'create the file if it doesnt exist
        If My.Computer.FileSystem.FileExists(path) = False Then
            Dim fs As System.IO.FileStream = File.Create(path)
            fs.Close()
        End If

        'same process as length of file
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim CurrentRow() As String
            Dim i As Integer
            While Not MyReader.EndOfData
                Try
                    CurrentRow = MyReader.ReadFields()
                    Dim CurrentFields As String
                    For Each CurrentFields In CurrentRow
                        A(i) = CInt(CurrentFields)
                        i += 1
                    Next
                Catch ex As Exception
                End Try
            End While
        End Using
    End Sub

    Function FormatTime(ByVal milliseconds As Integer)

        'converts a time in milliseconds to a string with the correct decimal place and leading zeros in seconds
        Dim temp As String = milliseconds.ToString

        'if the number of digits is less than 4 add leading zeros
        Select Case Len(temp)
            Case 1
                temp = "000" & temp
            Case 2
                temp = "00" & temp
            Case 3
                temp = "0" & temp
        End Select

        'convert the time to char array
        Dim Time() As Char = temp.ToCharArray
        Dim output As String = ""

        'loop through array in reverse order
        For i = Len(temp) - 1 To 0 Step -1

            'add the current digit to the output
            output = Time(i) & output

            'if the current index is 3 from the end then add a decimal point
            If i = Len(temp) - 3 Then
                output = "." & output
            End If
        Next

        'return the final string
        Return output & "s"
    End Function

    Function ValidateSort(ByVal File1 As String, ByVal File2 As String)

        'checks two files containg csv arrays to see if they have the same numbers in the same order

        'since all animation calls are made during a sort we need to manually disable the animation
        animateGraph = False
        Dim length1, length2 As Integer

        'find the length of the arrays from each of the files
        length1 = LengthOfFile(File1)
        length2 = LengthOfFile(File2)

        'if the lengths are not equal return false
        If length1 <> length2 Then Return False

        'create temp arrays to read the files
        Dim Array1(length1) As Integer
        Dim Array2(length2) As Integer

        'read the files
        ReadTextFile(Array1, File1)
        ReadTextFile(Array2, File2)

        'using a completely stable and in place sort, order both arrays into the right order
        BinaryInsertionSort(Array1, 0, length1)
        BinaryInsertionSort(Array2, 0, length2)

        'save the arrays for debugging
        savearray(Array1, length1, "Input.txt")
        savearray(Array2, length2, "Output.txt")

        'for all the index's check that each value is the same
        For i = 0 To length1
            If Array1(i) <> Array2(i) Then
                MsgBox(i.ToString & " = index where the sort clashed")
                Return False
            End If
        Next
        Return True
    End Function

    '---Events--- '

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'if the button is clicked the user wants to sort an array, since the graph updateing hogs the form1 thread so that the UI becomes completely unresponsive
        'call the sort subroutine on its own thread and set it to the back ground, minor effects to performance for massive UI improvements
        Dim BackgroundThread As New System.Threading.Thread(AddressOf Sort)
        BackgroundThread.IsBackground = True
        BackgroundThread.Start()
    End Sub

    Private Sub Form1_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged

        'change the size of the graph if the size of the form changes, until a limit at which point it is a size of 0
        Dim Height, Width As Integer
        If (Me.Size.Height - 10) > 0 And (Me.Size.Width - 140) > 0 Then
            Height = Me.Size.Height - 10
            Width = Me.Size.Width - 140
            Chart1.Size = New Size(Width, Height)
        End If
    End Sub

    Private Sub Form1_load(sender As System.Object, e As System.EventArgs) Handles Me.Load

        'on load initialize the labels and drop downs
        InitializeDropDowns()
        InitializeLabels()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged

        'changes the global animation variable fro mthe main thread so that the sorting Thread will update live
        animateGraph = CheckBox1.CheckState
    End Sub

    '---Threading Crap---'
    Private Sub ChartThreading(ByVal Threadinstructioncode As Integer)

        'this shit is so stupid but whatever

        'since calling any method from an object on a form requires doing so from the Thread which it was created you have to use delegates And invoking
        'to make calls from any thread i want, hence calling ChartThreading(1) from another Thread will cause an invoke so that the same call Is made but on
        'the thread it was oringally created
        Select Case Threadinstructioncode
            Case 1

                '1 is the same as clearing the series
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 1)
                Else
                    Chart1.Series.Clear()
                End If
            Case 2

                '2 is the same as clearing the chart area
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 2)
                Else
                    Chart1.ChartAreas.Clear()
                End If

            Case 3

                '3 will change the Y axis of the of the graph to be based on the global variables
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 3)
                Else

                    'calls the combobox value to the global variable
                    ComboBoxThreading(2)
                    With Chart1.ChartAreas.Add("ChartArea1")
                        .AxisX.Maximum = SizeA + 1
                        .AxisX.Minimum = 0
                        .AxisY.Minimum = minA - 1
                        Select Case ComboBoxItem

                            'change the y axis based on the size of the array
                            Case "Random"
                                .AxisY.Maximum = maxA
                            Case "Reverse"
                                .AxisY.Maximum = SizeA
                            Case "File"
                                .AxisY.Maximum = LargestFileValue
                            Case Else
                                .AxisY.Maximum = maxA
                        End Select
                    End With
                End If
            Case 4

                '4 will add the array series to the graph
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 4)
                Else
                    Chart1.Series.Add(" Array")
                End If
            Case 5

                '5 will update the graph
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 5)
                Else
                    Chart1.Update()
                End If
            Case 6

                '6 will remove the second series (1 index) from the legend
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 6)
                Else
                    Chart1.Series.Add(1).IsVisibleInLegend = False
                End If
            Case 7

                '7 will add the swapping series and change the colour of it
                If Chart1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ChartThreading)
                    Me.Invoke(d, 7)
                Else
                    Chart1.Series.Add("Swapping")
                    Chart1.Series("Swapping").Color = Color.Red
                End If
        End Select
    End Sub

    Private Sub Label1Threading(ByVal text As String)

        'changes the text of label 1 to parameter passed from any thread
        If Label1.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label1Threading)
            Me.Invoke(d, text)
        Else
            Label1.Text = text
            Label1.Refresh()
        End If
    End Sub

    Sub ComboBoxThreading(ByVal int As Integer)

        'sets the global variable for the drop downs to the value specified by the parameter From any thread
        Select Case int
            Case 1
                If ComboBox1.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ComboBoxThreading)
                    Me.Invoke(d, 1)
                Else
                    ComboBoxItem = (ComboBox1.SelectedItem)
                End If
            Case 2
                If ComboBox2.InvokeRequired Then
                    Dim d As New ThreadInt(AddressOf ComboBoxThreading)
                    Me.Invoke(d, 2)
                Else
                    ComboBoxItem = (ComboBox2.SelectedItem)
                End If
        End Select
    End Sub

    Private Sub ChartAddpoint(ByVal text As String, ByVal x As Integer, ByVal y As Integer)

        'adds a singular point to the seires specified by text
        If Chart1.InvokeRequired Then
            Dim d As New ThreadPoint(AddressOf ChartAddpoint)
            Me.Invoke(d, text, x, y)
        Else
            Chart1.Series(text).Points.AddXY(x, y)
        End If
    End Sub

    Private Sub BindArray(ByVal series As String, ByVal array() As Integer)

        'binds an entire array specified by the array passed to it to the series specified by the series string
        If Chart1.InvokeRequired Then
            Dim d As New ThreadBind(AddressOf BindArray)
            Me.Invoke(d, series, array)
        Else
            Chart1.Series(series).Points.DataBindY(array)
        End If
    End Sub

    Private Sub chartpointorigin()

        'adds a point of size 0 to point 0 (second series) so that the array series becomes evenly spaced out
        If Chart1.InvokeRequired Then
            Dim d As New Thread(AddressOf chartpointorigin)
            Me.Invoke(d)
        Else
            Chart1.Series(1).Points.AddXY(0, 0)
        End If
    End Sub

    'threading for updating labels'
    Private Sub Label20Threading(ByVal text As String)
        If Label20.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label20Threading)
            Me.Invoke(d, text)
        Else
            Label20.Text = text
            Label20.Refresh()
        End If
    End Sub
    Private Sub Label24Threading(ByVal text As String)
        If Label24.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label24Threading)
            Me.Invoke(d, text)
        Else
            Label24.Text = text
            Label24.Refresh()
        End If
    End Sub
    Private Sub Label25Threading(ByVal text As String)
        If Label25.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label25Threading)
            Me.Invoke(d, text)
        Else
            Label25.Text = text
            Label25.Refresh()
        End If
    End Sub
    Private Sub Label15Threading(ByVal text As String)
        If Label15.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label15Threading)
            Me.Invoke(d, text)
        Else
            Label15.Text = text
        End If
    End Sub
    Private Sub Label16Threading(ByVal text As String)
        If Label16.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label16Threading)
            Me.Invoke(d, text)
        Else
            Label16.Text = text
        End If
    End Sub
    Private Sub Label15ThreadingR()
        If Label15.InvokeRequired Then
            Dim d As New Thread(AddressOf Label15ThreadingR)
            Me.Invoke(d)
        Else
            Label15.Refresh()
        End If
    End Sub
    Private Sub Label16ThreadingR()
        If Label16.InvokeRequired Then
            Dim d As New Thread(AddressOf Label16ThreadingR)
            Me.Invoke(d)
        Else
            Label16.Refresh()
        End If
    End Sub
    Private Sub Label6Threading(ByVal text As String)
        If Label6.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label6Threading)
            Me.Invoke(d, text)
        Else
            Label6.Text = text
            Label6.Refresh()
        End If
    End Sub
    Private Sub Label17Threading(ByVal text As String)
        If Label17.InvokeRequired Then
            Dim d As New ThreadString(AddressOf Label17Threading)
            Me.Invoke(d, text)
        Else
            Label17.Text = text
        End If
    End Sub
    Private Sub Label17ThreadingR()
        If Label17.InvokeRequired Then
            Dim d As New Thread(AddressOf Label17ThreadingR)
            Me.Invoke(d)
        Else
            Label17.Refresh()
        End If
    End Sub

    Private Sub ReadTrackBar()
        If TrackBar1.InvokeRequired Then
            Dim d As New Thread(AddressOf ReadTrackBar)
            Me.Invoke(d)
        Else
            Trackbar = TrackBar1.Value
        End If
    End Sub
End Class