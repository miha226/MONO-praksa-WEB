

class Person {
    constructor(firstName, lastName, gender, workStatus) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.gender = gender;
    this.workStatus = workStatus;
    }
};

let persons = JSON.parse(localStorage.getItem('persons')) || [];
UpdateList();



function mySubmit() {
    let person = null;
    let fname = document.getElementById("fname").value;
    let lname = document.getElementById("lname").value;
    let genders =  document.getElementsByName("gender");
    for( var i=0, length = genders.length; i < length; i++ ){
        if(genders[i].checked){
            var gender = genders[i].value;
            break;
        }
    }
    let workStatus = document.getElementById("workStatus").value;
    if(fname != null && lname != null && workStatus != undefined && gender != undefined){
        person = new Person(fname, lname, gender, workStatus);
        persons.push(person);
        document.getElementById("personForm").reset();
        localStorage.clear();
        localStorage.setItem('persons',JSON.stringify(persons));
        UpdateList();
    }  
};



function UpdateList() {
    let list = document.getElementById("list");
    let personsGrid = document.getElementById("personsGrid");
    if(persons.length != 0){
        document.getElementById("listContainer").style.display = "block";
        
    }
    else{
        document.getElementById("listContainer").style.display = "none";
    } 
    list.innerHTML = "";
    personsGrid.innerHTML = "";
        persons.forEach((element,index) => {
            
            //list.innerHTML += "<li class='list-group-item'><a href='' onclick='DeleteItem(this)'>"+element.firstName+" "+element.lastName+"</a></li>";
            personsGrid.innerHTML += "<p style='grid-column: 1;'>"+element.firstName+"</p>"
            personsGrid.innerHTML += "<p style='grid-column: 2;'>"+element.lastName+"</p>"
            personsGrid.innerHTML += "<p style='grid-column: 3;'>"+element.gender+"</p>"
            personsGrid.innerHTML += "<p style='grid-column: 4;'>"+element.workStatus+"</p>"
            personsGrid.innerHTML += "<p style='grid-column: 5;'><a href='' onClick='DeleteItem("+index+")'>∰</a></p>"
        });
};

function ClearList(){
    localStorage.clear();
    persons = [];
    UpdateList();
}

function DeleteItem(id){
    persons.splice(id,1);
    localStorage.clear();
    localStorage.setItem('persons',JSON.stringify(persons));
}