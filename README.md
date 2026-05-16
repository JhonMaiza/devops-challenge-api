# DevOps Challenge API

## Descripción General

Este proyecto implementa un microservicio REST desarrollado con ASP.NET Core 8 siguiendo buenas prácticas DevOps y principios de automatización CI/CD.

La solución incluye:

- API REST con autenticación JWT
- Validación mediante API Key
- Pruebas automatizadas y cobertura de código
- Análisis estático de código
- Containerización con Docker
- Despliegues multi-entorno
- Balanceador de carga con NGINX
- Pipeline CI/CD con Azure DevOps

---

# Arquitectura

## Entorno de Producción

```text
                    ┌─────────────────┐
                    │     Cliente     │
                    └────────┬────────┘
                             │
                             ▼
                  ┌────────────────────┐
                  │ NGINX LoadBalancer │
                  └────────┬───────────┘
                           │
               ┌───────────┴───────────┐
               ▼                       ▼
      ┌────────────────┐     ┌────────────────┐
      │      app1      │     │      app2      │
      │ ASP.NET Core   │     │ ASP.NET Core   │
      └────────────────┘     └────────────────┘
```

---

# Tecnologías Utilizadas

- C#
- ASP.NET Core 8
- Docker
- Docker Compose
- NGINX
- xUnit
- Azure DevOps
- GitHub
- JWT Authentication

---

# Endpoint de la API

## POST `/DevOps`

### Request

```json
{
  "message": "This is a test",
  "to": "Juan Perez",
  "from": "Rita Asturia",
  "timeToLifeSec": 45
}
```

### Response

```json
{
  "message": "Hello Juan Perez your message will be sent"
}
```

---

# Seguridad

La API implementa:

- Validación mediante API Key
- Autenticación JWT
- Generación única de JWT por transacción

## Headers requeridos

```text
X-Parse-REST-API-Key
X-JWT-KWY
```

---

# Ejecución de la Aplicación

## Entorno de Desarrollo

### Levantar entorno DEV

```bash
docker compose -f docker-compose.dev.yml up -d --build
```

---

## Entorno de Producción

### Levantar entorno PROD

```bash
docker compose -f docker-compose.prod.yml up -d --build
```

---

# Prueba del Endpoint

## Ejemplo de Request con cURL

```bash
curl -X POST \
  -H "X-Parse-REST-API-Key: 2f5ae96c-b558-4c7b-a590-a501ae1c3f6c" \
  -H "X-JWT-KWY: YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{ 
        "message": "This is a test",
        "to": "Juan Perez",
        "from": "Rita Asturia",
        "timeToLifeSec": 45
      }' \
  http://localhost:8081/DevOps
```

---

# Pruebas Automatizadas

La solución incluye pruebas automatizadas utilizando xUnit.

## Ejecutar pruebas

```bash
dotnet test
```

---

# Cobertura de Código

Los reportes de cobertura son generados automáticamente durante la ejecución del pipeline.

## Ejecutar cobertura localmente

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Análisis Estático de Código

El análisis estático se implementa mediante:

```bash
dotnet format --verify-no-changes
```

Esta validación se ejecuta automáticamente en el pipeline CI/CD.

---

# Docker Multi-Stage Build

La aplicación utiliza un Docker multi-stage build para:

- Reducir el tamaño final de la imagen
- Separar entornos de compilación y ejecución
- Mejorar el rendimiento de despliegue
- Seguir buenas prácticas de containerización

---

# Pipeline CI/CD

El pipeline CI/CD se implementa utilizando Azure DevOps.

## Etapas del Pipeline

```text
Build
↓
Test
↓
Deploy Development
↓
Deploy Production
```

---

# Características del Pipeline

El pipeline incluye:

- Gestión de dependencias
- Builds automatizados
- Pruebas automatizadas
- Cobertura de código
- Análisis estático
- Despliegue automatizado con Docker
- Despliegue automático a producción desde la rama master
- Soporte para ejecución manual
- Soporte para ejecución basada en versiones mediante tags Git

---

# Múltiples Entornos

La solución soporta múltiples entornos de despliegue.

## Desarrollo

Utiliza:

```text
docker-compose.dev.yml
```

## Producción

Utiliza:

```text
docker-compose.prod.yml
```

---

# Despliegues Versionados

El pipeline soporta despliegues basados en tags Git.

## Ejemplo

```bash
git tag v1.0
git push origin v1.0
```

---

# Escalabilidad

La aplicación soporta escalabilidad horizontal mediante replicación de contenedores detrás de un balanceador de carga NGINX.

Nuevos nodos pueden agregarse fácilmente utilizando Docker Compose o plataformas de orquestación como Kubernetes.

---

# Balanceador de Carga

NGINX es utilizado como:

- Reverse Proxy
- Balanceador de carga
- API Gateway ligero

El tráfico se distribuye entre múltiples nodos utilizando balanceo round-robin.

---

# Estructura del Repositorio

```text
.
├── azure-pipelines.yml
├── docker-compose.dev.yml
├── docker-compose.prod.yml
├── default.conf
├── README.md
├── DevOpsApi
└── DevOpsApi.Tests
```

---

# Autor

Jhon Maiza