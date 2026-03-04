import { Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

export interface ServiceCategory {
  name: string;
  services: string[];
}

@Injectable({
  providedIn: 'root'
})
export class SeoService {
  private serviceCategories: ServiceCategory[] = [
    {
      name: 'Manicure',
      services: ['Manicure żelowy', 'Manicure hybrydowy', 'Manicure klasyczny', 'Manicure tradycyjny']
    },
    {
      name: 'Pedicure',
      services: ['Pedicure hybrydowy', 'Pedicure klasyczny', 'Pedicure tradycyjny', 'Pedicure SPA']
    },
    {
      name: 'Brwi i Rzęsy',
      services: ['Laminacja brwi', 'Laminacja rzęs', 'Henna brwi', 'Henna rzęs', 'Regulacja brwi']
    },
    {
      name: 'Zabiegi Kosmetyczne',
      services: ['Oczyszczanie wodorowe', 'Peeling LINDER HEALTH', 'BioRePeelCl3', 'Masaż twarzy']
    },
    {
      name: 'Depilacja',
      services: ['Depilacja woskiem', 'Depilacja twarzy', 'Depilacja pach', 'Depilacja nóg', 'Depilacja bikini']
    },
    {
      name: 'Mezoterapia Mikroigłowa',
      services: ['Mezoterapia mikroigłowa', 'Egzosomy DIVES', 'BioRePeel z mezoterapią']
    }
  ];

  constructor(
    private meta: Meta,
    private title: Title
  ) { }

  updateTitle(title: string): void {
    this.title.setTitle(title);
  }

  updateMeta(name: string, content: string): void {
    this.meta.updateTag({ name, content });
  }

  updateOpenGraph(property: string, content: string): void {
    this.meta.updateTag({ property, content });
  }

  setStructuredData(data: any): void {
    const script = document.createElement('script');
    script.type = 'application/ld+json';
    script.text = JSON.stringify(data);
    document.head.appendChild(script);
  }

  setPageSeo(data: {
    title: string;
    description: string;
    image?: string;
    url?: string;
    localBusiness?: boolean;
  }): void {
    this.updateTitle(`${data.title} | G&P Atelier Urody`);

    this.updateMeta('description', data.description);
    this.updateMeta('og:description', data.description);

    this.updateMeta(
      'keywords',
      'salon piękności, salon kosmetyczny, manicure, pedicure, brwi, rzęsy, laminacja, zabiegi kosmetyczne, depilacja, mezoterapia, Mikołów, Tychy, Katowice, Paula Rumin, Gabriela Włoszek'
    );

    this.updateOpenGraph('og:title', data.title);
    this.updateOpenGraph('og:type', 'website');

    if (data.image) this.updateOpenGraph('og:image', data.image);
    if (data.url) this.updateOpenGraph('og:url', data.url);

    if (data.localBusiness) {
      this.setStructuredData({
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [
          {
            "@type": "ListItem",
            "position": 1,
            "name": "Home",
            "item": "https://beautysalon.pl"
          }
        ]
      });
    }
  }

  setServicePageSeo(): void {
    this.setPageSeo({
      title: 'Nasze Usługi',
      description:
        'G&P Atelier Urody - pełna oferta: manicure, pedicure, zabiegi na brwi i rzęsy, zabiegi kosmetyczne, peeling, depilacja, mezoterapia mikroigłowa, masaż w Mikołowie. Rezerwacja na Booksy!',
      url: 'https://beautysalon.pl/offer'
    });

    this.setServiceCategoriesSchema();
  }

  setAboutPageSeo(): void {
    this.setPageSeo({
      title: 'O Nas',
      description:
        'G&P Atelier Urody - salon piękności prowadzony przez Gabrielę Włoszek i Paulę Rumin. Profesjonalne zabiegi kosmetyczne w Mikołowie, Tychach, Katowicach',
      url: 'https://beautysalon.pl/about'
    });
  }

  setContactPageSeo(): void {
    this.setPageSeo({
      title: 'Kontakt',
      description:
        'Skontaktuj się z G&P Atelier Urody, Podleska 31/2, Mikołów. Manicure, pedicure, zabiegi na brwi i rzęsy, zabiegi kosmetyczne. Rezerwacja online na Booksy!',
      url: 'https://beautysalon.pl/contact'
    });
  }

  getServiceCategories(): ServiceCategory[] {
    return this.serviceCategories;
  }

  setServiceCategoriesSchema(): void {
    const services = this.serviceCategories.flatMap(category =>
      category.services.map(service => ({
        "@context": "https://schema.org",
        "@type": "Service",
        "name": service,
        "provider": {
          "@type": "LocalBusiness",
          "name": "G&P Atelier Urody",
          "url": "https://beautysalon.pl",
          "address": {
            "@type": "PostalAddress",
            "streetAddress": "Podleska 31/2",
            "addressLocality": "Mikołów",
            "addressRegion": "Śląskie",
            "postalCode": "43-190",
            "addressCountry": "PL"
          }
        },
        "description": `${service} w G&P Atelier Urody. Profesjonalny zabieg wykonany przez doświadczonych specjalistów. Rezerwacja na Booksy.`,
        "areaServed": ["Mikołów", "Tychy", "Katowice"]
      }))
    );

    if (services.length > 0) {
      this.setStructuredData(services[0]);
    }
  }

  setLocalBusinessSchema(data: {
    name: string;
    address: string;
    phone: string;
    email: string;
    image: string;
  }): void {
    const schema = {
      '@context': 'https://schema.org',
      '@type': 'LocalBusiness',
      'name': data.name,
      'image': data.image,
      'address': {
        '@type': 'PostalAddress',
        'streetAddress': data.address,
        'addressCountry': 'PL'
      },
      'telephone': data.phone,
      'email': data.email,
      'url': 'https://beautysalon.pl'
    };

    const script = document.createElement('script');
    script.type = 'application/ld+json';
    script.textContent = JSON.stringify(schema);
    document.head.appendChild(script);
  }
}
