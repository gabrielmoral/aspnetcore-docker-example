FROM mariadb

ENV MYSQL_DATABASE="valuesDb"
ENV MYSQL_USER="anuser"
ENV MYSQL_PASSWORD="apassword"
ENV MYSQL_ROOT_PASSWORD="rootpasswd"

COPY init.sql /docker-entrypoint-initdb.d/init.sql