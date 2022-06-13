import {Prodavnica} from "./Prodavnica.js"

var glavniDiv=document.createElement("div");
glavniDiv.className="glavniDiv";
document.body.appendChild(glavniDiv);

fetch("https://localhost:5001/Ispit/PreuzmiProdavnice",{
    method:"GET",
})
.then(p=>p.json().then(data=>{
data.forEach(element => {    

var Prod=new Prodavnica(element.id,element.naziv);
Prod.crtaj(glavniDiv);
}
)}));
        
