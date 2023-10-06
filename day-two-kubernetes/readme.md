Install 
- Cert-Manager
```bash
helm install cert-manager jetstack/cert-manager --namespace cert-manager --set installCRDs=true --create-namespace
```

- Redis
```bash
helm install redis -n redis bitnami/redis -f .\values.dev.yaml --create-namespace
```

- PostgreSQL 15.4
```bash
helm install postgresql -f .\values.dev.yaml -n postgresql-15-4 bitnami/postgresql --create-namespace
```

- PostgreSQL 9.6
```bash
helm install postgresql -f .\values.dev.yaml -n postgresql-9-6 bitnami/postgresql --create-namespace
```

- Portainer
```bash
helm install portainer -n portainer portainer/portainer --create-namespace --set service.type=ClusterIP
```
```bash
helm install portainer  -f .\values.dev.yaml -n portainer portainer/portainer --create-namespace --set service.type=ClusterIP
```

- ArgoCD
```bash
helm install argo -f .\values.dev.yaml -n argo-cd bitnami/argo-cd --create-namespace
```

- RabbitMQ
```bash
helm install rabbitmq -n rabbitmq -f .\values.dev.yaml bitnami/rabbitmq
```

- Proxy forward
```bash
kubectl port-forward --namespace <namespace> svc/<svc name> <outbound port>:<inbound port>
```

- Jaeger API Tracing
```bash
helm install jaeger -n jaeger bitnami/jaeger --create-namespace
```

- Kong
* requied 
 - PostgreSQL 9.6
```bash 
helm install kong -n kong kong/kong -f .\values.dev.yaml --create-namespace
```

- Konga
```bash
helm install konga -n konga ./ -f .\values.dev.yaml --create-namespace
```

Kafka
```bash
helm install kafka -n kafka -f .\values.dev.yaml bitnami/kafka --create-namespace
```