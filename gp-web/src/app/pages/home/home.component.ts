import { Component, OnInit } from '@angular/core';
import { SeoService } from '../../services/seo.service';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private seo: SeoService) { }

  ngOnInit(): void {
    this.seo.setPageSeo({
      title: 'G&P Atelier Urody - Salon kosmetyczny | Manicure, Pedicure, Zabiegi',
      description:
        'G&P Atelier Urody - salon kosmetyczny w Mikołowie, Tychach, Katowicach. Profesjonalne manicure, pedicure, zabiegi na brwi i rzęsy, laminacja, zabiegi kosmetyczne, depilacja, mezoterapia. Paula Rumin, Gabriela Włoszek.',
      image: 'https://beautysalon.pl/beauty-salon.jpg',
      url: 'https://beautysalon.pl'
    });

    this.seo.setLocalBusinessSchema({
      name: 'G&P Atelier Urody',
      address: 'Podleska 31/2, 43-190 Mikołów',
      phone: '+48 660 068 943',
      email: 'gp_atelierurody@o2.pl',
      image: 'https://via.placeholder.com/400x300'
    });
  }
}
