# Search API

This is a Webapi with two main methods

http://**application domain**/Search/Exams?criteria=**search terms**

This method will query document in Azure Search using the fields Name and Synonym of the index. it Will return the document which has any of the search terms terms provided.

Return a Json with the documents found.

http://**application domain**/Search/ExamsMode?criteria=**search terms**&searchmode=**search mode**

This method will query document in Azure Search using the fields Name and Synonym of the index.

Parameters:
1. **criteria**
    The terms that will be used to perform the search.

2. **searchmode**
    integer, can accept 0 or 1.
    
    **searchmode = 0**, will use any word in the term to perform the search
    **searchmode = 1**, will use All the words.
