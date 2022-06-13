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

        dugme.onclick=(ev)=>{
            var izabranPr=this.kontejner.querySelector('select[name="selektProizvoda"]').value;
            var izabranaKolicina=this.kontejner.querySelector(".kolicina").value;

            fetch("https://localhost:5001/Ispit/Poruci/"+this.id+"/"+izabranPr+"/"+izabranaKolicina,
            {
                method:"PUT",
                headers:{
                    "Content-Type":"application/json"
                },
            }).then(p=>{if(p.ok){p.json().then(data=>{
                data.forEach(elem => {
                    var divSastojakKojiNedostaje=document.createElement("div");
                    divSastojakKojiNedostaje.innerHTML=elem.naziv+" "+elem.kolicina;//////

                    divPrikaz.appendChild(divSastojakKojiNedostaje);
                })
            })

            }
            })
        }
    }
}

       