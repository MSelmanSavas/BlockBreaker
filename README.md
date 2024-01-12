Though Process and Planning can be tracked here!
https://app.milanote.com/1Rl1ek1DyQze53?p=gaofwUUactV

## To Do's
- Pool sistemini temiz ve modüler bir şekilde implemente edebilme şansım olmadı.
- UI Management tarafı biraz sona kaldığı ve zaman sıkıştığı için biraz dağınık bir yapıda oldu.
- Genel ve hızlı bir game config sistemi eklenip, static bir class üzerinden gerekli configler hızlıca çekilebilirdi.
- Entity Datalarının ve patlayabilen blokların hasarlarının config tarafından set ettirilebilmesi eklenebiliyor.
- Entity üzerindeki dataların işlem yapmaması adına entity data ve entity aksiyonları olarak ikiye bölünebilirdi. Entity data kısmı var, ama entity actionları generic bir yapıda implemente edecek vaktim olmadı malesef.
- Belki sistemler arasındaki dependency leri bir DI sistemi kullanarak daha temiz şekilde sağlayabilirdim.

Proje üzerindeki genel değişkenlere Assets/Configs altından ulaşılabilir.
Patlayabilen blokların patlama alanları, verdikleri hasar ve tüm blokların canlarına Assets/Prefabs/Blocks altındaki prefablardan ulaşılabilir.

Oyunun genel giriş noktası GameLoader üzerinden yapılmaktadır. GameLoader içerisindeki Initialize fonksiyonu tarafından oyundaki sistemler takip edilebilir.
