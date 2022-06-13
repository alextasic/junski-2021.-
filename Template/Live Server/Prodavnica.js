///import { Proizvod } from "./Proizvod.js";
export class Prodavnica{
    constructor (id,naziv){
    this.id=id;
    this.naziv=naziv;
    this.kontejner=null;
    }
    crtaj(host)
    {
        var divCelogPrikazaProdavnice=document.createElement("div");
        host.appendChild(divCelogPrikazaProdavnice);
        divCelogPrikazaProdavnice.className="divCelogPrikazaProdavnice";
        divCelogPrikazaProdavnice.innerHTML=this.naziv;
        this.kontejner=divCelogPrikazaProdavnice;

        var divUnos=document.createElement("div");
        divCelogPrikazaProdavnice.appendChild(divUnos);
        divUnos.className="divUnos";

        var divPrikaz=document.createElement("div");//divPrikazNedostajucihSastojaka
        divCelogPrikazaProdavnice.appendChild(divPrikaz);
        divPrikaz.className="divPrikaz";
        divPrikaz.innerHTML="Nabavka";

        this.crtajUnos(divUnos,divPrikaz);
    }
    crtajUnos(host,divPrikaz){

        let labelaPorucivanje=document.createElement("label");
        labelaPorucivanje.innerHTML="Porucivanje";
        host.appendChild(labelaPorucivanje);

        let labelaPr=document.createElement("label");
        labelaPr.innerHTML="Proizvod";
        host.appendChild(labelaPr);

        var selektProizvoda=document.createElement("select");
        host.appendChild(selektProizvoda);
        selektProizvoda.name="selektProizvoda";

        fetch("https://localhost:5001/Ispit/PreuzmiProizvode/"+this.id,{
            method:"GET",
        }
        ).then(p=>
            {if (p.ok){
                p.json().then(data=>{
                    data.forEach(element => {
                        
                        var opcija=document.createElement("option");
                        opcija.innerHTML=element.naziv;
                        opcija.value=element.id;
                        selektProizvoda.appendChild(opcija);
                    });
                })
            }
        })

        var kolicina=document.createElement("input");
        kolicina.className="kolicina";
        kolicina.type="number";
        host.appendChild(kolicina);

        var dugme=document.createElement("button");
        dugme.innerHTML="poruci";
        host.appendChild(dugme);

        var dugmePoruci=document.createElement("button");
        dugmePoruci.innerHTML="poruci ono sto nedostaje";
        divPrikaz.appendChild(dugmePoruci);
        var divPrikaz1=document.createElement("div");
        divPrikaz1.className="divPrikaz1";
        divPrikaz.appendChild(divPrikaz1);
        

        dugme.onclick=(ev)=>{
            var izabranPr=this.kontejner.querySelector('select[name="selektProizvoda"]').value;
            var izabranaKolicina=this.kontejner.querySelector(".kolicina").value;

           /* var trazenjeDivaZaPrikaz1=document.querySelector(".divPrikaz1");
            console.log(trazenjeDivaZaPrikaz1);
            (trazenjeDivaZaPrikaz1.parentNode).removeChild(trazenjeDivaZaPrikaz1);///nadje se div koji moze da se obrise 
            
            var divPrikaz1=document.createElement("div");
                    divPrikaz1.className="divPrikaz1";
                    divPrikaz.appendChild(divPrikaz1);*/

            fetch("https://localhost:5001/Ispit/Poruci/"+this.id+"/"+izabranPr+"/"+izabranaKolicina,
            {
                method:"PUT",
                headers:{
                    "Content-Type":"application/json"
                },
            }).then(p=>{if(p.ok)
                {p.json().then(data=>{
                data.forEach(elem => {
                   
                    var SastojakKojiNedostaje=document.createElement("input");
                  
                    SastojakKojiNedostaje.type="checkbox";
                    SastojakKojiNedostaje.value=elem.sastojak.id;
                    SastojakKojiNedostaje.name="sastojciKojiNedostaju";
                   
                    let labela=document.createElement("label");
                    labela.innerHTML=elem.sastojak.naziv+" "+elem.kolicina;//////
                    divPrikaz1.appendChild(SastojakKojiNedostaje);
                    divPrikaz1.appendChild(labela);
                })

                dugmePoruci.onclick=(ev)=>{
                    var izabraniIDProizvoda=this.kontejner.querySelector('input[name="sastojciKojiNedostaju"]').value;

                fetch("https://localhost:5001/Ispit/MenadzerPorucuje/"+this.id+"/"+izabraniIDProizvoda,//+"/"+izabranaKolicina,
                {
                method:"PUT",
                headers:{
                    "Content-Type":"application/json"
                },
                }).then(p=>{if(p.ok){
                    alert("naruceni proizvod!");
                    }
                    else {
                    alert("nije narucen proizvod");
                    }
                    })


                    }
                })

                }
            else {alert("uspesno narucena proizvod")}
            })
        }
    }
}

       