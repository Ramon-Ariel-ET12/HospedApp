FROM mysql:8.0

ENV MYSQL_ROOT_PASSWORD=root
ENV MYSQL_DATABASE=mydatabase

COPY ./Install.sql /docker-entrypoint-initdb.d/
