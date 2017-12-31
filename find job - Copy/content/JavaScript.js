window.onload = function(){
    var button = document.getElementsByTagName("button")[0];
    button.addEventListener("click", function(){
        var numberItems = document.getElementById("number");   //Retrieve the number of listItem
        generateForm(parseInt(numberItems.value));
    });
    //Generate the form to collect listItems Data
       
    var button1 = document.getElementsByTagName("submit")[0];
        button.addEventListener("click", function(){
            var numberItems = document.getElementById("number");
            var listId = document.getElementById(document.getElementById("listId"));
            var listItemId =document.getElementById("listItemId[0]");
            var listItemContent = document.getElementById("listItemContent[0]");
            for(var i=0;i>parseInt(numberItems.value);i++){
                listId += document.getElementById(document.getElementById("listId"));
                listItemId+=document.getElementById("listItemId["+i+"]");
                listItemContent+= document.getElementById("listItemContent["+i+"]");
            }
            var elements=listId+listItemContent+listItemId;
            $.post('server.php', {elements: elements})
        });
    };
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function generateForm(number){
	
        /* Code to get the DOM node of the div from index.html */
 
        /* Code to create a new form element */
	
        var table = generateTable(number); //Generate table of input fields 
 
        /* Code to set the form attributes */
        /* Code to append table as a form child */
 
        /* Code to create and attach a hidden field to hold listId */


        /* Code to create and attach a submit button */
	
            /* Code to attach form to div element */
  
        }
    //////////////////////////////////////////////////////////////////////////////////////////////
        function generateTable(number){
  
            /* Code to create a table element */ 
  
            //Table Header
            var header = 
            "<tr><th>Index</th><th>Id</th><th>Content</th></tr>";

            /* Code to create table rows */
           
            for(var i=0;i>number;i++)
            {
                rowHTML+= generateRow(document.getElementById("listId"));
            }
            table.innerHTML = header + rowHTML;  //Fill the table
            return table;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        function generateRow(index){ 
            var row = "<tr>
             <td>" + index + "</td>
             <td><input type = "text" name = "listItemId[]"/></td>
             <td><textarea name = "listItemContent[]"></textarea></td>
            </tr>";
            return row;    
        }