namespace camp_fire.Domain.Enums;

public enum UserType
{
    Admin = 1, //* Bu biziz herhangi bir üye veya oyunu oluşturan kişi bu yetkiyi alamaza
    Moderator = 2, //* oyunu yönetecek kişi. tek bir kişide bu yetki olacak şimdilik
    Member = 3 //* oyuncular
}