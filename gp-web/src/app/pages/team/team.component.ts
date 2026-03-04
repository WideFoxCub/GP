import { Component, OnInit } from '@angular/core';
import { SeoService } from '../../services/seo.service';

@Component({
  selector: 'app-team',
  standalone: true,
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  constructor(private seo: SeoService) {}

  ngOnInit(): void {
    this.seo.setPageSeo({
      title: 'Nasz Zespół',
      description: 'Poznaj nasz zespół - Gabriela Włoszek i Paula Rumin. Doświadczeni profesjonaliści salon kosmetyczny w Mikołowie',
      url: 'https://beautysalon.pl/team'
    });
  }
}
