import { Routes } from '@angular/router';
import { FormularioClienteComponent } from './clientes/formulario-cliente/formulario-cliente.component';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./clientes/listar/listar.component').then(m => m.ListarComponent)
    },
    {
        path: 'clientes',
        loadComponent: () => import('./clientes/listar/listar.component').then(m => m.ListarComponent)
    },
    { 
        path: 'formulario-cliente', 
        loadComponent: () => import('./clientes/formulario-cliente/formulario-cliente.component').then(m => m.FormularioClienteComponent)  
    },
    { 
        path: 'formulario-cliente/:idCliente', 
        loadComponent: () => import('./clientes/formulario-cliente/formulario-cliente.component').then(m => m.FormularioClienteComponent)  
    },
];
