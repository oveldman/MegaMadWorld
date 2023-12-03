## Create secrets
* Copy and rename Secrets-prod-example.yaml to Secrets-prod.yaml
  * Fill all the secrets

**Generate Seq password:**
```bash
echo 'p@ssw0rd' | docker run --rm -i datalust/seq config hash
```