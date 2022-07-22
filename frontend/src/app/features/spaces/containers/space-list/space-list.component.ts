import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from "@angular/core";
import {BehaviorSubject, combineLatest, Observable, of, Subject, Subscription, switchMap, tap} from "rxjs";
import {Page} from "../../../../shared/models/page.model";
import {SpaceList} from "../../../../shared/models/space.model";
import {SpaceService} from "../../../../core/services/space.service";
import {GetSpacesQuery} from "../../../../shared/queries/space.query";

@Component({
  selector: "app-space-list",
  templateUrl: "./space-list.component.html",
  styleUrls: ["./space-list.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpaceListComponent implements OnInit, OnDestroy {
  private _subscriptions = new Subscription();

  private _getSpaces = new Subject<GetSpacesQuery>();

  private _spaces = new BehaviorSubject<SpaceList[]>([]);
  private _totalAmount = new BehaviorSubject<number>(0);
  private _currentPage = new BehaviorSubject<Page>({pageIndex: 0, pageSize: 10});

  public spaces$: Observable<SpaceList[]>;
  public totalAmount$: Observable<number>;
  public currentPage$: Observable<Page>;

  public constructor(spaceService: SpaceService) {
    this.spaces$ = this._spaces.asObservable();
    this.totalAmount$ = this._totalAmount.asObservable();
    this.currentPage$ = this._currentPage.asObservable();

    const getSpacesSubscription = this._getSpaces.pipe(
      switchMap(query => combineLatest([spaceService.get(query), of(query)])),
      tap(([{spaces, totalAmount}, {pageIndex, pageSize}]) => {
        this._currentPage.next({pageIndex, pageSize});
        this._spaces.next(spaces);
        this._totalAmount.next(totalAmount);
      })
    ).subscribe();

    this._subscriptions.add(getSpacesSubscription);
  }

  public getSpaces({pageIndex, pageSize}: Page): void {
    this._getSpaces.next({pageIndex, pageSize});
  }

  public ngOnInit(): void {
    this.getSpaces(this._currentPage.value);
  }

  public ngOnDestroy(): void {
    this._subscriptions.unsubscribe();
  }
}
