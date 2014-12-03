﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SpelDatabas;

namespace SpelAffarWCF
{
    [DataContract]
    public class SpelAffarService : ISpelAffarService
    {
        public List<ProduktDto> HämtaProduktViaGenre(int genreId)
        {
            using (var db = new SpelDatabasContainer())
            {
                var prod = db.GetProductsByGenre(genreId).ToList();
                var prodDto = new ProduktDto(prod.First());
                //var prodId = prod.FirstOrDefault().Id;
                var prodId = prodDto.Id;
                var genreList = new List<int>();
                var consoleList = new List<int>();
                var returnList = new List<ProduktDto>();
                foreach (var spel in prod)
                {
                    if (prodId != spel.Id)
                    {
                        returnList.Add(prodDto);
                        prodId = spel.Id;
                        prodDto = new ProduktDto(spel);
                        genreList = new List<int>();
                        consoleList = new List<int>();
                    }
                    if (!genreList.Contains((int)spel.GenreId))
                    {
                        var genre = db.GenreSet.Where(f => f.Id == spel.GenreId).Select(f => new GenreDto { Id = f.Id, Namn = f.Namn }).First();
                        prodDto.Genres.Add(genre);
                        genreList.Add((int)spel.GenreId);
                    }
                    if (!consoleList.Contains((int)spel.KonsolId))
                    {
                        var console = db.KonsolSet.Where(f => f.Id == spel.KonsolId).Select(f => new KonsolDto { Id = f.Id, Namn = f.Namn }).First();
                        prodDto.Konsoler.Add(console);
                        consoleList.Add((int)spel.KonsolId);
                    }
                }

                returnList.Add(prodDto);

                return returnList;
                //var prod = (from m in db.ProduktSet
                //            select m).ToList();
                // var products = (from s in db.GetProductsByGenre(Convert.ToInt32(genreId))

                //                let konsoler = s.Konsol.Select(konsol => new KonsolDto
                //                 {
                //                     Id = konsol.Id,
                //                     Namn = konsol.Namn
                //                 }).ToList()
                //         let genres = s.Genre.Select(genre => new GenreDto
                //         {
                //             Id = genre.Id,
                //             Namn = genre.Namn
                //         }).ToList()
                //         let spelPerOrders = s.SpelPerOrder.Select(spo => new SpelPerOrderDto
                //         {
                //             SpelId = spo.SpelId,
                //             OrderId = spo.OrderId,
                //             Antal = spo.Antal
                //         }).ToList() 
                //         select new ProduktDto
                //         {
                //             Id = s.Id,
                //             Beskrivning = s.Beskrivning,
                //             Namn = s.Namn,
                //             Pris = s.Pris,
                //             Beställningar = s.Beställningar,
                //             Konsoler = konsoler,
                //             Betyg = s.Betyg,
                //             Genres = genres,
                //             SpelPerOrders = spelPerOrders,
                //             Singleplayer = s.Singleplayer,
                //             Multiplayer = s.Multiplayer,
                //             Utgivningsår = s.Utgivningsår,
                //             Utgivare = (from u in db.UtgivareSet
                //                         where u.Id == s.UtgivareId
                //                         select new UtgivareDto
                //                         {
                //                             Id = u.Id,
                //                             Namn = u.Namn
                //                         }).FirstOrDefault()
                //         }).Distinct().ToList();
                // return products;
            }
        }

        public List<ProduktDto> HämtaProdukter()
        {
            using (var db = new SpelDatabasContainer())
            {
                var list = (from produkt in db.ProduktSet
                            let konsoler = produkt.Konsol.Select(konsol => new KonsolDto
                            {
                                Id = konsol.Id,
                                Namn = konsol.Namn
                            }).ToList()
                            let genres = produkt.Genre.Select(genre => new GenreDto
                            {
                                Id = genre.Id,
                                Namn = genre.Namn
                            }).ToList()
                            let spelPerOrders = produkt.SpelPerOrder.Select(spo => new SpelPerOrderDto
                            {
                                SpelId = spo.SpelId,
                                OrderId = spo.OrderId,
                                Antal = spo.Antal
                            }).ToList()
                            select new ProduktDto
                            {
                                Beställningar = produkt.Beställningar,
                                Pris = produkt.Pris,
                                Betyg = produkt.Betyg,
                                Id = produkt.Id,
                                Namn = produkt.Namn,
                                Beskrivning = produkt.Beskrivning,
                                Multiplayer = produkt.Multiplayer,
                                Singleplayer = produkt.Singleplayer,
                                Konsoler = konsoler,
                                Genres = genres,
                                SpelPerOrders = spelPerOrders,
                                Utgivare = new UtgivareDto
                                {
                                    Id = produkt.Utgivare.Id,
                                    Namn = produkt.Utgivare.Namn
                                },
                                Utgivningsår = produkt.Utgivningsår
                            }).ToList();
                return list;
            }
        }

        public ProduktDto HämtaProdukt(int produktId)
        {
            using (var db = new SpelDatabasContainer())
            {
                return (from produkt in db.ProduktSet
                        let konsoler = produkt.Konsol.Select(konsol => new KonsolDto
                        {
                            Id = konsol.Id,
                            Namn = konsol.Namn
                        }).ToList()
                        let genres = produkt.Genre.Select(genre => new GenreDto
                        {
                            Id = genre.Id,
                            Namn = genre.Namn
                        }).ToList()
                        let spelPerOrders = produkt.SpelPerOrder.Select(spo => new SpelPerOrderDto
                        {
                            SpelId = spo.SpelId,
                            OrderId = spo.OrderId,
                            Antal = spo.Antal
                        }).ToList()
                        select new ProduktDto
                        {
                            Beställningar = produkt.Beställningar,
                            Betyg = produkt.Betyg,
                            Pris = produkt.Pris,
                            Id = produkt.Id,
                            Namn = produkt.Namn,
                            Beskrivning = produkt.Beskrivning,
                            Konsoler = konsoler,
                            Genres = genres,
                            Multiplayer = produkt.Multiplayer,
                            Singleplayer = produkt.Singleplayer,
                            SpelPerOrders = spelPerOrders,
                            Utgivare = new UtgivareDto
                            {
                                Id = produkt.Utgivare.Id,
                                Namn = produkt.Utgivare.Namn
                            },
                            Utgivningsår = produkt.Utgivningsår
                        }).FirstOrDefault();
            }
        }

        public PersonDto KollaKund(string firstName, string lastName, string logOnEmail, string lösenord)
        {
            using (var db = new SpelDatabasContainer())
            {
                SpelDatabas.Personer person = null;
                if (db.PersonerSet.ToList().Exists(x => x.LogOnEmail == logOnEmail))
                    person = db.PersonerSet.FirstOrDefault(x => x.LogOnEmail == logOnEmail);
                else
                {
                    person = new SpelDatabas.Personer
                    {
                        Förnamn = firstName,
                        Efternamn = lastName,
                        LogOnEmail = logOnEmail,
                        Lösenord = lösenord,
                        Order = new List<Order>()
                    };

                    db.PersonerSet.Add(person);
                    db.Entry(person).State = EntityState.Added;
                    db.SaveChanges();
                }

                var personDto = new PersonDto
                {
                    Förnamn = person.Förnamn,
                    Efternamn = person.Efternamn,
                    LogOnEmail = person.LogOnEmail,
                    Lösenord = person.Lösenord,
                    Id = person.Id,
                };

                if (person.Order != null)
                {
                    foreach (var order in person.Order)
                    {
                        var orderDto = new OrderDto();
                        var spelOrderDto = new SpelPerOrderDto();

                        foreach (var spelOrder in order.SpelPerOrder)
                        {
                            spelOrderDto.Antal = spelOrder.Antal;
                            spelOrderDto.OrderId = spelOrder.OrderId;
                            spelOrderDto.SpelId = spelOrder.SpelId;
                        }
                        orderDto.Datum = order.Datum;
                        orderDto.Id = order.Id;
                        orderDto.Kommentar = order.Kommentar;
                        orderDto.PersonId = order.PersonerId;
                        orderDto.SpelPerOrders.Add(spelOrderDto);
                    }
                }

                return personDto;
            }
        }

        public List<ProduktDto> GetTopListGames(int antal)
        {
            using (var db = new SpelDatabasContainer())
            {
                //var list = new List<ProduktDto>();
                //foreach (var item in db.GetTopListGames(antal).ToList())
                //{
                //    list.Add(new ProduktDto
                //    {
                //        Id = item.Id,
                //        Namn = item.Namn,
                //        Beställningar = item.Beställningar,
                //        Utgivningsår = item.Utgivningsår
                //    });
                //}
                return new List<ProduktDto>();
            }
        }

        public OrderDto NyOrder(int kundId, int[] produkter, string kommentar)
        {
            using (var db = new SpelDatabasContainer())
            {
                var person = db.PersonerSet.FirstOrDefault(x => x.Id == kundId);
                if (person != null)
                {
                    var personDto = new PersonDto
                    {
                        Id = person.Id,
                        Förnamn = person.Förnamn,
                        Efternamn = person.Efternamn,
                        LogOnEmail = person.LogOnEmail,
                        Lösenord = person.Lösenord,
                        Ordrar = new List<OrderDto>()
                    };
                    var order = new Order
                    {
                        Datum = DateTime.Now,
                        PersonId = person.Id,
                        PersonerId = person.Id,
                        Kommentar = kommentar,
                        SpelPerOrder = new List<SpelPerOrder>()
                    };
                    db.OrderSet.Add(order);
                    db.Entry(order).State = EntityState.Added;

                    db.SaveChanges();
                    var orderDto = new OrderDto
                    {
                        Id = order.Id,
                        Datum = order.Datum,
                        Kommentar = order.Kommentar,
                        PersonId = order.PersonId,
                        SpelPerOrders = new List<SpelPerOrderDto>()
                    };
                    foreach (var produkt in produkter.Select(pId => (from p in db.ProduktSet
                                                                     where pId == p.Id
                                                                     select p).FirstOrDefault()).Where(prod => prod != null))
                    {
                        var spelPerOrder = new SpelPerOrder { OrderId = order.Id };
                        var spelPerOrderDto = new SpelPerOrderDto { OrderId = order.Id };

                        spelPerOrderDto.Antal++; // ska väl matcha hur många av samma element som fanns i int[] med spel?
                        spelPerOrderDto.SpelId = produkt.Id;
                        spelPerOrderDto.OrderId = order.Id;
                        orderDto.SpelPerOrders.Add(spelPerOrderDto);

                        //produkt.Beställningar++;
                        db.Entry(produkt).State = EntityState.Modified;

                        spelPerOrder.Produkt = produkt;
                        spelPerOrder.Produkt.Beställningar++; // ska väl också matcha hur många av samma element som fanns i int[] med spel?
                        spelPerOrder.Order = order;
                        spelPerOrder.Antal++;
                        spelPerOrder.SpelId = produkt.Id;
                        order.SpelPerOrder.Add(spelPerOrder);

                        db.OrderSet.Add(order);
                        db.SpelPerOrderSet.Add(spelPerOrder);

                        db.Entry(spelPerOrder).State = EntityState.Added;
                        db.Entry(order).State = EntityState.Added;
                    }
                    person.Order.Add(order);
                    personDto.Ordrar.Add(orderDto);

                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    return orderDto;
                }
                return new OrderDto();
            }
        }
    }
}
