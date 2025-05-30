"use strict";

// This loads helper components from the Extended Component Library,
// https://github.com/googlemaps/extended-component-library.
import {APILoader} from 'https://ajax.googleapis.com/ajax/libs/@googlemaps/extended-component-library/0.6.11/index.min.js';

const CONFIGURATION = {
  "ctaTitle": "Ubicar",
  "mapOptions": {"center":{"lat":37.4221,"lng":-122.0841},"fullscreenControl":true,"mapTypeControl":false,"streetViewControl":true,"zoom":11,"zoomControl":true,"maxZoom":22,"mapId":""},
  "mapsApiKey": "YOUR_API_KEY_HERE",
  "capabilities": {"addressAutocompleteControl":true,"mapDisplayControl":true,"ctaControl":true}
};

const SHORT_NAME_ADDRESS_COMPONENT_TYPES =
    new Set(['street_number', 'administrative_area_level_1', 'postal_code']);

const ADDRESS_COMPONENT_TYPES_IN_FORM = [
  'location',
  'locality',
  'administrative_area_level_1',
  'postal_code',
  'country',
];

function getFormInputElement(componentType) {
  return document.getElementById(`${componentType}-input`);
}

function fillInAddress(place) {
  function getComponentName(componentType) {
    for (const component of place.address_components || []) {
      if (component.types[0] === componentType) {
        return SHORT_NAME_ADDRESS_COMPONENT_TYPES.has(componentType) ?
            component.short_name :
            component.long_name;
      }
    }
    return '';
  }

  function getComponentText(componentType) {
    return (componentType === 'location') ?
        `${getComponentName('street_number')} ${getComponentName('route')}` :
        getComponentName(componentType);
  }

  for (const componentType of ADDRESS_COMPONENT_TYPES_IN_FORM) {
    getFormInputElement(componentType).value = getComponentText(componentType);
  }
}

function renderAddress(place) {
  const mapEl = document.querySelector('gmp-map');
  const markerEl = document.querySelector('gmp-advanced-marker');

  if (place.geometry && place.geometry.location) {
    mapEl.center = place.geometry.location;
    markerEl.position = place.geometry.location;
  } else {
    markerEl.position = null;
  }
}

async function initMap() {
  const {Autocomplete} = await APILoader.importLibrary('places');

  const mapOptions = CONFIGURATION.mapOptions;
  mapOptions.mapId = mapOptions.mapId || 'DEMO_MAP_ID';
  mapOptions.center = mapOptions.center || {lat: 37.4221, lng: -122.0841};

  await customElements.whenDefined('gmp-map');
  document.querySelector('gmp-map').innerMap.setOptions(mapOptions);
  const autocomplete = new Autocomplete(getFormInputElement('location'), {
    fields: ['address_components', 'geometry', 'name'],
    types: ['address'],
  });

  autocomplete.addListener('place_changed', () => {
    const place = autocomplete.getPlace();
    if (!place.geometry) {
      // User entered the name of a Place that was not suggested and
      // pressed the Enter key, or the Place Details request failed.
      window.alert(`No details available for input: '${place.name}'`);
      return;
    }
    renderAddress(place);
    fillInAddress(place);
  });
}

initMap();