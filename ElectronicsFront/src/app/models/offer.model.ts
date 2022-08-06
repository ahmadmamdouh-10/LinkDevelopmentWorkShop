export interface Offer{
    OfferInformation : OfferInformation,
    ProductDetails :ProductDetails
}

export interface OfferInformation{
    Store: string,
    StoreCurrency: string,
    Id: number,
    PriceAfterDiscount: number,
    Discount: number,
    Title: number,
    Description: string,
    ImageUrl: string,
    ExpirationDateTime: Date
}

export interface ProductDetails{
    Id: number,
    Title: string,
    Price: number,
    ImagePath: string,
    Code: string,
    Rate: number
}
